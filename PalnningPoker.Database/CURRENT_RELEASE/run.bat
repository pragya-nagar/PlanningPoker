

echo OFF

SET server=COMPND-STTFSP\SQL2012STDSP1
SET user=botdbusr
SET pwd=#Ty7Hfg$123
SET db=BOTCARD_DEV

Sqlcmd -S "COMPND-STTFSP\SQL2012STDSP1" -U "botdbusr" -P "#Ty7Hfg$123" -q "BACKUP DATABASE [BOTCARD_DEV] TO DISK = N'D:\BACKUP\BOTCARD_DEV.BAK' WITH NOFORMAT, NOINIT,  NAME = N'BOTCARD_DEV-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"

echo  running ddl

sqlcmd -S %server% -U %user% -P %pwd% -d %db% -o ddl.log    -i ddl.sql

echo  running dml

sqlcmd -S %server% -U %user% -P %pwd% -d %db% -o dml.log    -i dml.sql

exit



























