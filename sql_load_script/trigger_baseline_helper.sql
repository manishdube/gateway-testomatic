REM DELETE from ILOGADMIN.DB_BUILD_HISTORY
SET DEFINE OFF;
Delete from ILOGADMIN.DB_BUILD_HISTORY where build_log = '9.99.999.999';

REM INSERT INTO ILOGADMIN.DB_BUILD_HISTORY
INSERT INTO "ILOGADMIN"."DB_BUILD_HISTORY" (BUILD_LOG, BUILD_DATE) VALUES ('9.99.999.999', SYSDATE);   
COMMIT;