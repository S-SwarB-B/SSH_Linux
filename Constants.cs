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
            public const string MemoryProblem = "MEMORY:";
            public const string StorageProblem = "STORAGE:";
            public const string CPUProblem = "CPU:";
            public const string NetworkProblem = "Network:";
            public const string SyncProblem = "Sync:";
            public const string SystimeProblem = "Systime:";
        }

        //Сообщения решенных ошибок
        // # MessageSuccessfulConnect - Сообщение о удачном подключении
        // # MessageSuccessfulFile - Сообщение о найденном файле
        // # MessageSuccessfulProblem - Сообщение о решении проблемы
        internal static class SolutionsProblem
        {
            public const string MessageSuccessfulConnect = "Удалось установить подключение";
            public const string MessageSuccessfulFile = "Файл запуска программы найден";
            public const string MessageSuccessfulProblem = "Проблема была решена";
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

        //Критичность (Первые четыре пункта опасно трогать)
        internal static class ErrorImportanceClass
        {
            internal static class Critical //Критичная
            {
                public const double Memory = 90;
                public const double Storage = 90;
                public const double CPU = 90;
            }
            internal static class VeryHight //Очень выскокая
            {
                public const double Memory = 80;
                public const double Storage = 80;
                public const double CPU = 80;
            }
            internal static class Hight //Выскокая
            {
                public const double Memory = 70;
                public const double Storage = 70;
                public const double CPU = 70;
            }
            internal static class Medium //Умеренная
            {
                public const double Memory = 60;
                public const double Storage = 60;
                public const double CPU = 60;
            }
            internal static class ImportanceAdditional // 2 - Medium; 3 - Hight; 4 - VeryHight; 5 - Critical
            {
                public const int Sync = 2;
                public const int Network = 2;
                public const int SysTime = 2;
            }
        }
    }
}
