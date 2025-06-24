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
        }

        //Сообщения решенных ошибок
        // # MessageSuccessfulConnect - Сообщение о удачном подключении
        // # MessageSuccessfulFile - Сообщение о найденном файле
        // # MessageSuccessfulProblem - Сообщение о решении проблемы
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
            internal static class ImportanceAdditional //1 - Low; 2 - Medium; 3 - Hight; 4 - VeryHight; 5 - Critical
            {
                public const int Sync = 2;
                public const int Network = 2;
                public const int SysTime = 2;
            }
        }
    }
}
