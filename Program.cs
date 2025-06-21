using System.Reflection;
using SSHProject;

SSHContext sc = new SSHContext();

SSHConnect conectSSH = new SSHConnect(); //Вызов метода

List<Server> serverList = sc.Servers.ToList(); //Получение серверов из базы

ProblemDeleteInterval.DeleteSolutionProblem(sc, Constants.DeleteTime.DeleteParameterIntevalHour);
ProblemDeleteInterval.DeleteParameters(sc, Constants.DeleteTime.DeleteParameterIntevalHour);

Parallel.ForEach(serverList, serverInformation => //Паралельный вызов для каждого из серверов
{
    conectSSH.SSH(serverInformation.IdServer, serverInformation.IpAdress, Constants.LogIn.Login, //Подключение к серверам
    Constants.LogIn.Password, Constants.LogIn.Path, sc);
});