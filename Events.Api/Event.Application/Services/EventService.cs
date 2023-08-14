using Event.Application.Interface;
using Event.Application.Models;
using Event.Application.Repo;

namespace Event.Application.Services
{
    public class EventService : IEvent
    {
        public IEventRepo _eventRepo;
        public EventService()
        {
            _eventRepo = new EventRepoSingleton();
        }

        public BaseResponse CreateEvent(EventCreateRequest request)
        {
            var response = new BaseResponse();
            try
            {
                if (request == null || string.IsNullOrEmpty(request.User) || string.IsNullOrEmpty(request.Subject) || request.Date == null)
                {
                    response.Message = "Provide Valid Request";
                    return response;
                }
                _eventRepo.AddEvent(new EventDetails(request));
                response.IsSuccess = true;
                response.Message = "Created Succesfully";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public BaseResponse UpdateEvent(EventDetails request)
        {
            var response = new BaseResponse();
            try
            {
                if (request == null || string.IsNullOrEmpty(request.User) || string.IsNullOrEmpty(request.Subject) || request.Date == null)
                {
                    response.Message = "Provide Valid Request";
                    return response;
                }
                _eventRepo.UpdateEvent(request);
                response.IsSuccess = true;
                response.Message = "Updated Succesfully";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public EventGetResponse GetEvent(int eventId)
        {
            EventGetResponse response = new();
            try
            {
                if (eventId <= 0)
                {
                    response.Message = "Provide Valid Event Id.";
                    return response;
                }
                var eventDetails = _eventRepo.GetEvent(eventId);
                if (eventDetails != null)
                {
                    response.EventDetails = eventDetails;
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = "Event Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public EventGetAllResponse GetAllEvents()
        {
            EventGetAllResponse response = new();
            try
            {
                var events = _eventRepo.GetAllEvents();
                if (events != null && events.Any())
                {
                    response.TodayEvents = events.Where(a => a.Date != null && a.Date.Value == DateOnly.FromDateTime(DateTime.Now)).ToList();
                    response.UpcomingEvents = events.Where(a => a.Date != null && a.Date.Value > DateOnly.FromDateTime(DateTime.Now)).ToList();
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public BaseResponse DeleteEvent(int eventId)
        {
            BaseResponse response = new();
            try
            {
                if (eventId <= 0)
                {
                    response.Message = "Provide Valid Event Id.";
                    return response;
                }
                var eventDetails = _eventRepo.DeleteEvent(eventId);
                if (eventDetails)
                {
                    response.IsSuccess = true;
                    response.Message = "Event Deleted Successfully.";
                }
                else
                {
                    response.Message = "Event Not Found.";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

