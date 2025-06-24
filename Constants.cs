using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal static class Constants
    {
        //Константы подключения к серверам и получения из них данных
        // # Login - Логин для захода на сервер
        // # Password - Пароль для захода на сервер
        // # Path - Путь к файлу запуска на сервере
        internal static class LogIn
        {
            public const string Login = "braws";
            public const string Password = "1234567890";
            public const string Path = "shopt -s globstar && cd ~/**/ServerMonitoringAgent/ServerMonitoringAgent/ServerMonitoringAgent && dotnet run";
        }

        //Константы периодичности удаления данных в БД
        // # DeleteSolutionProblemIntevalDay - Через сколько дней очищать решенные проблемы
        // # DeleteParameterIntevalHour - Через сколько часов очищать данные по параметрам
        internal static class DeleteTime
        {
            public const int DeleteSolutionProblemIntevalDay = 3;
            public const int DeleteParameterIntevalHour = 24;
        }

        //Сообщения ошибок (Перед изменением нужно очистить базу данных)
        // # MessageFailedConnect - Сообщение о неудачном подключении
        // # MessageFailedFile - Сообщение о не найденном файле
        // # MemoryProblem - Сообщение о занятости оперативной памяти
        // # StorageProblem - Сообщение о занятости памяти диска
        // # CPUProblem - Сообщение о нагрузке центрального процессора
        // # NetworkProblem - Сообщение о неправильности IP
        // # SyncProblem - Сообщение о неправильной синхронизации
        // # SystimeProblem - Сообщение о проблеме с временем
        // # DemonDockerProblem - Сообщение о незапущенном демоне докера
        // # SduProblem - Сообщение о незапущенном контейнере SDU
        // # FileStorageProblem - Сообщение о непримонтированном файловом хранище
        // # ContainerPostgresProblem - Сообщение о незапущенном контейнере Postgres
        // # ETCDProblem - Сообщение о незапущенном контейнере ETCD
        // # DDMWebAdminProblem - Сообщение о незапущенном контейнере DDMWebAdmin
        // # DDMWebUiProblem - Сообщение о незапущенном контейнере DDMWebUi
        // # DDMWebApiProblem - Сообщение о незапущенном контейнере DDMWebApi
        internal static class ActiveProblem
        {
            public const string MessageFailedConnect = "Подключение не установлено";
            public const string MessageFailedFile = "Файл запуска программы не найден";
            public const string MemoryProblem = "MEMORY";
            public const string StorageProblem = "STORAGE";
            public const string CPUProblem = "CPU";
            public const string NetworkProblem = "Айпи не соответсвует требованиям";
            public const string SyncProblem = "Проблема с синхронизацией времени";
            public const string SystimeProblem = "Проблема с системным временем";
            public const string DemonDockerProblem = "Демон докера не запущен";
            public const string SduProblem = "Контейнер sdu не запущен";
            public const string FileStorageProblem = "Файловое хранилище не примонтировано";
            public const string ContainerPostgresProblem = "Контейнер postgres не запущен";
            public const string ETCDProblem = "Контейнер etcd не запущен";
            public const string DDMWebAdminProblem = "Контейнер ddmwebadmin-ui не запущен";
            public const string DDMWebUiProblem = "Контейнер ddmweb-ui не запущен";
            public const string DDMWebApiProblem = "Контейнер ddmwebapi не запущен";
        }

        //Сообщения решенных ошибок
        internal static class SolutionsProblem
        {
            public const string MessageSuccessfulConnect = "Удалось установить подключение";
            public const string MessageSuccessfulFile = "Файл запуска программы найден";
            public const string MessageSuccessfulMemory = "Проблема с нагрузкой оперативной памяти была решена";
            public const string MessageSuccessfulStorage = "Проблема с нагрузкой диска была решена";
            public const string MessageSuccessfulCPU = "Проблема с нагрузкой центрального процессора была решена";
            public const string MessageSuccessfulNetwork = "Проблема с айпи была решена";
            public const string MessageSuccessfulSync = "Проблема с синхронизацией времени была решена";
            public const string MessageSuccessfulSystime = "Проблема с системным временем была решена";
            public const string MessageSuccessfulDemonDocker = "Демон докера смог запуститься";
            public const string MessageSuccessfulSdu = "Контейнер sdu смог запуститься";
            public const string MessageSuccessfulFileStorage = "Файловое хранилище получилось примонтировать";
            public const string MessageSuccessfulContainerPostgres = "Контейнер postgres запустился";
            public const string MessageSuccessfulETCD = "Контейнер etcd запустился";
            public const string MessageSuccessfulDDMWebAdmin = "Контейнер ddmwebadmin-ui запустился";
            public const string MessageSuccessfulDDMWebUi = "Контейнер ddmweb-ui запустился";
            public const string MessageSuccessfulDDMWebApi = "Контейнер ddmwebapi запустился";
        }


        //Теги (Лучше не трогать)
        internal static class Tags
        {
            public const string TagCPU = "[CPU]";
            public const string TagMEMORY = "[MEMORY]";
            public const string TagSTORAGE = "[STORAGE]";
            public const string TagSYSTIME = "[SYSTIME]";
            public const string TagSYNCTIME = "[SYNCTIME]";
            public const string TagNETWORK = "[NETWORK]";
            public const string TagDEMONDOCKER = "ffff";
            public const string TagCONSDU = "ffff";
            public const string TagFILESTORAGE = "ffff";
            public const string TagCONPOSTGRES = "ffff";
            public const string TagCONETCD = "ffff";
            public const string TagDDMWEBADMIN = "ffff";
            public const string TagDDMWEB = "ffff";
            public const string TagDDMWEBAPI = "ffff";
        }

        //Критичность
        internal static class ErrorImportanceClass //Стандартные параметры лучше не менять
        {
            internal static class ImportanceStandart //Для стандартного мониторинга (CPU, MEMORY, STORAGE)
            {
                public const double Critical = 90;
                public const double VeryHight = 80;
                public const double Hight = 70;
                public const double Medium = 50;
                public const double Low = 50;
                public const double VeryLow = 45;
            }
            internal static class ImportanceAdditional //0 - VeryLow; 1 - Low; 2 - Medium; 3 - Hight; 4 - VeryHight; 5 - Critical
            {
                public const int Sync = 2;
                public const int Network = 2;
                public const int SysTime = 2;
                public const int DemonDocker = 2;
                public const int ContainerSDU = 2;
                public const int FileStorage = 2;
                public const int ContainerPostrges = 2;
                public const int ConteinerETCD = 2;
                public const int DDMWebAdmin = 2;
                public const int DDMWeb = 2;
                public const int DDMWebApi = 2;
            }
        }
    }
}
