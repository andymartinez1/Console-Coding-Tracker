using Coding_Tracker.Services;
using Coding_Tracker.Views;

namespace Coding_Tracker.Controllers;

public class CodingController
{
    private readonly ICodingService _codingService;

    public CodingController(ICodingService codingService)
    {
        _codingService = codingService;
    }

    public void GetAllSessions()
    {
        var sessions = _codingService.GetAllSessions();

        UserInterface.ViewAllSessions(sessions);
    }

    public void AddSession()
    {
        _codingService.AddSession();
    }

    public void UpdateSession(int id)
    {
        _codingService.UpdateSession(id);
    }

    public void DeleteSession(int id)
    {
        _codingService.DeleteSession(id);
    }
}
