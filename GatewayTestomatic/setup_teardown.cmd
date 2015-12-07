@echo off
rem
rem %1 = ilogadmin password
IF "%1"=="" ECHO "DEFINE ILOGADMIN PASSWORD" & GOTO :eof
rem %2 = database instance
IF "%2"=="" ECHO "DEFINE DATABASE INSTANCE" & GOTO :eof
set DB_INST=%2

echo executing user creation script ... %runDate% %runtime%											                            
exit|sqlplus ilogadmin/%1@%DB_INST% @GW_USERCREATION_SCRIPT.sql 
echo finished user creation script ... %runDate% %runtime%	


