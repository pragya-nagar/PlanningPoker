using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace PlanningPoker.Repository.Class
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly AppDBContext _context;
        public UserStoryRepository(AppDBContext context)
        {
            _context = context;
        }
        public List<UserStory> Import(IFormFile formFile, int gameId, int userId)
        {
            var gameSessionList = new List<GameSession>();
            var gameSession = new GameSession();
            gameSession.GameId = gameId;
            gameSession.UpdatedBy = userId;
            gameSession.CreatedBy = userId;
            gameSession.CreatedOn = DateTime.Now;
            gameSession.UpdatedOn = DateTime.Now;
            gameSession.RowGuid = Guid.NewGuid();
            gameSession.SessionTime = DateTime.Now;
            gameSessionList.Add(gameSession);

            foreach (var list in gameSessionList)
            {
                _context.GameSession.Add(list);
            }
            _context.SaveChanges();

            var userstoryList = new List<UserStory>();
            try
            {
                if ((formFile != null) && (formFile.Length > 0) && !string.IsNullOrEmpty(formFile.FileName))
                {
                    string fileName = formFile.FileName;
                    string fileContentType = formFile.ContentType;
                    byte[] fileBytes = new byte[formFile.Length];
                    using (var stream = new MemoryStream())
                    {
                        formFile.CopyToAsync(stream);
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage(stream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                var userstory = new UserStory();
                                userstory.GameId = gameId;
                                userstory.UserStory1 = workSheet.Cells[rowIterator, 1].Value.ToString();
                                userstory.Description = workSheet.Cells[rowIterator, 2].Value.ToString();
                                userstory.GameSessionId = gameSession.GameSessionId;
                                userstory.CreatedOn = DateTime.Now;
                                userstory.UpdatedOn = DateTime.Now;
                                userstory.CreatedBy = userId;
                                userstory.UpdatedBy = userId;
                                userstory.RowGuid = Guid.NewGuid();
                                userstoryList.Add(userstory);
                            }
                        }
                    }

                    foreach (var item in userstoryList)
                    {
                        _context.UserStory.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return new List<UserStory>();
            }

            return userstoryList;
        }

        public PageResult<UserStoryInsertRequest> GetByGameId(long gameId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var list = new List<UserStoryInsertRequest>();
            var story = _context.UserStory.Where(x => x.GameId == gameId).ToList();
            var skipAmount = pageSize * pageIndex;
            var totalRecords = story.Count;
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;


            var result = new PageResult<UserStoryInsertRequest>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;
            var PagedQuery = story.OrderBy(x => x.UserStoryId).Skip(skipAmount).Take(pageSize).ToList();
            if (PagedQuery.Count > 0)
            {
                foreach (var stories in PagedQuery)
                {
                    list.Add(new UserStoryInsertRequest
                    {
                        UserStoryId = stories.UserStoryId,
                        GameId = stories.GameId,
                        Story = stories.UserStory1,
                        Description = stories.Description
                    });
                }
            }
            result.Results = list;
            return result;
        }

        public PageResult<UserStoryInsertRequest> SearchStory(long gameId, string story, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var list = new List<UserStoryInsertRequest>();
            var search = _context.UserStory.Where(x => x.UserStory1.Contains(story) && x.GameId == gameId).ToList();
            var skipAmount = pageSize * pageIndex;
            var totalRecords = search.Count;
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;

            var result = new PageResult<UserStoryInsertRequest>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;

            var PagedSearch = search.OrderBy(x => x.UserStoryId).Skip(skipAmount).Take(pageSize).ToList();
            if (PagedSearch.Count > 0)
            {
                foreach (var stories in PagedSearch)
                {
                    list.Add(new UserStoryInsertRequest
                    {
                        UserStoryId = stories.UserStoryId,
                        GameId = stories.GameId,
                        Story = stories.UserStory1,
                        Description = stories.Description
                    });
                }
            }

            result.Results = list;
            return result;
        }

        public UserStory GetByUserStoryId(long userstoryId)
        {
            return _context.UserStory.Find(userstoryId);
        }
    }
}
