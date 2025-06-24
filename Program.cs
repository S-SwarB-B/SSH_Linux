using System.Reflection;
using SSHProject;
using SSHProject.DB;

DateTime startProgram = DateTime.Now;

ServerMonitoringContext scDel = new ServerMonitoringContext();

SSHConnect conectSSH = new SSHConnect(); //Вызов метода

List<Server> serverList = scDel.Servers.ToList(); //Получение серверов из базы

ProblemDeleteInterval.DeleteSolutionProblem(scDel, Constants.DeleteTime.DeleteParameterIntevalHour); //Установка интервала очистки решенных проблем
ProblemDeleteInterval.DeleteParameters(scDel, Constants.DeleteTime.DeleteParameterIntevalHour); //Установка интервала очистки решенных проблем

Parallel.ForEach(serverList, serverInformation => //Паралельный вызов для каждого из серверов
{
    ServerMonitoringContext sc = new ServerMonitoringContext();
    conectSSH.SSH(serverInformation.Id, serverInformation.IpAddres, Constants.LogIn.Login, //Подключение к серверам
    Constants.LogIn.Password, Constants.LogIn.Path, sc, startProgram);
});