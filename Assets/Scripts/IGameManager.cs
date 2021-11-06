public interface IGameManager {
	ManagerStatus status {get;}

	void Startup(NetworkService service); // метод Startup теперь принимает один параметр - вставленный объект
}
