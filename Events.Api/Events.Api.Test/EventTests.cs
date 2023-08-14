using Event.Application.Models;
using Event.Application.Services;
using Events.Api.Controllers.EventController;

namespace Events.Api.Test;

public class EventTests
{
    EventController _controller;
    [SetUp]
    public void Setup()
    {
        _controller = new EventController(new EventService());
    }

    [Test]
    public void InvalidRequestTest()
    {
        var request = new EventCreateRequest();
        var response = _controller.CreateEvent(request);
        Assert.That(response.IsSuccess, Is.EqualTo(false));
        Assert.That(response.Message, Is.EqualTo("Provide Valid Request"));
    }

    [Test]
    public void ValidRequestTest()
    {
        var request = new EventCreateRequest();
        request.User = "test";
        request.Subject = "test";
        request.Description = "test";
        request.Date = DateOnly.FromDateTime(DateTime.Now);
        request.StartTime = "1:00:00";
        request.EndTime = "2:00:00";
        var response = _controller.CreateEvent(request);
        Assert.That(response.IsSuccess, Is.EqualTo(true));
        Assert.That(response.Message, Is.EqualTo("Created Succesfully"));
    }

    [Test]
    public void ValidUpComingRequestTest()
    {
        var request = new EventCreateRequest();
        request.User = "test";
        request.Subject = "test";
        request.Description = "test";
        request.Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
        request.StartTime = "1:00:00";
        request.EndTime = "2:00:00";
        var response = _controller.CreateEvent(request);
        Assert.That(response.IsSuccess, Is.EqualTo(true));
        Assert.That(response.Message, Is.EqualTo("Created Succesfully"));
    }

    [Test]
    public void ValidAllEventsTest()
    {
        CreateTask(true);
        CreateTask();
        var response = _controller.GetAllEvents();
        Assert.That(response.IsSuccess, Is.EqualTo(true));
        Assert.That(response.TodayEvents.Any(), Is.EqualTo(true));
        Assert.That(response.UpcomingEvents.Any(), Is.EqualTo(true));
    }

    private void CreateTask(bool today= false)
    {
        var request = new EventCreateRequest();
        request.User = "test";
        request.Subject = "test";
        request.Description = "test";
        if(today)
        {
            request.Date = DateOnly.FromDateTime(DateTime.Now);
        } else
        {
            request.Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
        }
        request.StartTime = "1:00:00";
        request.EndTime = "2:00:00";
        _controller.CreateEvent(request);
    }

    [Test]
    public void ValidDeleteTest()
    {
        CreateTask();
        var response = _controller.DeleteEvent(1);
        Assert.That(response.IsSuccess, Is.EqualTo(true));
        Assert.That(response.Message, Is.EqualTo("Event Deleted Successfully."));
    }

    [Test]
    public void InvalidDeleteTest()
    {
        var response = _controller.DeleteEvent(99999);
        Assert.That(response.IsSuccess, Is.EqualTo(false));
        Assert.That(response.Message, Is.EqualTo("Event Not Found."));
    }

    [Test]
    public void InvalidDeleteTest2()
    {
        var response = _controller.DeleteEvent(0);
        Assert.That(response.IsSuccess, Is.EqualTo(false));
        Assert.That(response.Message, Is.EqualTo("Provide Valid Event Id."));
    }

    [Test]
    public void ValidUpdateRequestTest()
    {
        CreateTask(true);
        var events = _controller.GetAllEvents();
        var request = events.TodayEvents.First();
        request.Subject = "Event Updated";
        var response = _controller.UpdateEvent(request);
        Assert.That(response.IsSuccess, Is.EqualTo(true));
        Assert.That(response.Message, Is.EqualTo("Updated Succesfully"));
        var updated = _controller.GetAllEvents();
        Assert.That(updated.TodayEvents.FirstOrDefault(a=>a.Id == request.Id)?.Subject, Is.EqualTo("Event Updated"));
    }
}
