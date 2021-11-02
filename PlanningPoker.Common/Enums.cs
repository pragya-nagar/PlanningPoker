using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Common
{
    public class Enums
    {
        public enum RoleName
        {
            SuperAdmin = 0,
            NotAssigned = 1,
            ScrumMaster = 2,
            ProductOwner = 3,
            Developer = 4,
        }
        public enum GameStatus
        {
            Played = 1,
            Scheduled = 2,
            Start = 3,
            PlayingNow = 4
        }
        public enum Status
        {
            Assigned = 1,
            NotAssigned = 0
        }
        public enum UserAcceptance
        {
            IsAccepted = 0,
            NullResponse = 1
        }
        public enum KeyWordType
        {
            RetrievedAreas = 0,
            DefinitionOfEstimation = 1
        }
        public enum ReturnCodes
        {
            ParameterError = 0,
            GenericException = 1,
            VerifySecretKeySucceeded = 2,
            VerifySecretKeyError = 3,
            DataCreateSucceeded = 100,
            DataCreatePartiallySucceeded = 101,
            DataUpdateSucceeded = 102,
            DataUpdatePartiallySucceeded = 103,
            DataRemoveSucceeded = 104,
            DataRemovePartiallySucceeded = 105,
            DataGetSucceeded = 106,
            DataCreateFailed = 200,
            DataCreateFailedWithDuplicateData = 201,
            DataCreateFailedWithErrorRelationships = 202,
            DataUpdateFailed = 203,
            DataUpdateFailedWithDuplicateData = 204,
            DataUpdateFailedWithErrorRelationships = 205,
            DataRemoveFailed = 206,
            DataGetFailed = 207,
            DataGetFailedWithErrorRelationships = 208,
            DataGetFailedWithNoData = 209,
        }
    }
}
