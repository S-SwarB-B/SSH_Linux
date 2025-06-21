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
        internal static class LogIn
        {
            public const string Login = "braws";
            public const string Password = "1234567890";
            public const string Path = "shopt -s globstar && cd ~/**/ServerMonitoringAgent/ServerMonitoringAgent/ServerMonitoringAgent && dotnet run";
        }
        
        //Константы периодичности удаления данных в БД
        internal static class DeleteTime
        {
            public const int DeleteSolutionProblemIntevalDay = 3;
            public const int DeleteParameterIntevalHour = 24;
        }

        //Теги
        internal static class Tags
        {
            public const string TagCPU = "[CPU]";
            public const string TagMEMORY = "[MEMORY]";
            public const string TagSTORAGE = "[STORAGE]";
            public const string TagSYSTIME = "[SYSTIME]";
            public const string Tag1 = "";
            public const string Tag2 = "";
        }

        //Сообщения решенных ошибок
        internal static class SolutionsProblem
        {
            public const string MessageSuccessfulConnect = "Удалось установить подключение";
            public const string MessageSuccessfulFile = "Файл запуска программы найден";
            public const string MessageSuccessfulProblem = "Проблема была решена";
        }

        //Сообщения ошибок
        internal static class ActiveProblem
        {
            public const string MessageFailedConnect = "Подключение не установлено";
            public const string MessageFailedFile = "Файл запуска программы не найден";
            public const string MemoryProblem = "MEMORY:";
            public const string StorageProblem = "STORAGE:";
            public const string CPUProblem = "CPU:";
            public const string SyncProblem = "Sync = NO";
            public const string DataProblem = "Разница времени:";
            public const string Problem1 = "";
        }

        //Критичность
        internal static class ErrorImportanceClass
        {
            internal static class Critical
            {
                public const double Memory = 90;
                public const double Storage = 90;
                public const double CPU = 90;
            }
            internal static class VeryHight
            {
                public const double Memory = 80;
                public const double Storage = 80;
                public const double CPU = 80;
            }
            internal static class Hight
            {
                public const double Memory = 70;
                public const double Storage = 70;
                public const double CPU = 70;
            }
            internal static class Medium
            {
                public const double Memory = 60;
                public const double Storage = 60;
                public const double CPU = 60;
            }
        }
    }
}
