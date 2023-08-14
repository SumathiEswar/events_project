import React, { useEffect, useState } from "react";
import { DeleteEvent, GetAllEvents } from "../../constants/rest-api-constant";
import { FetchApiPromise } from "../../services/api-service";
import "./EventList.css";

function EventList({ editEvent }: any) {
  const [eventResponse, setEventResponse] = useState([] as any);
  useEffect(() => {
    getAllEvents();
  }, []);
  const getAllEvents = async () => {
    FetchApiPromise(GetAllEvents, "GET")
      .then((response: any) => response.json())
      .then((data) => {
        setEventResponse(data);
      })
      .catch((error) => {
        // Handle any errors
        setEventResponse(error);
        console.error("Error:", error);
      });
  };
  const deleteEvent = (evt: any) => {
    FetchApiPromise(DeleteEvent + evt.id, "DELETE")
      .then((response: any) => response.json())
      .then((data) => {
        if (data && data.isSuccess) {
          getAllEvents();
        }
      })
      .catch((error) => {
        // Handle any errors
        setEventResponse(error);
        console.error("Error:", error);
      });
  };

  const eventDetails = (evt: any) => {
    const eventDate = new Date(evt.date).toDateString().split(" ");
    const startTime = new Date(evt.date + " " + evt.startTime).toLocaleString(
      "en-US",
      { hour: "numeric", minute: "numeric", hour12: true }
    );
    const endTime = new Date(evt.date + " " + evt.endTime).toLocaleString(
      "en-US",
      { hour: "numeric", minute: "numeric", hour12: true }
    );
    return (
      <tr className="events-row" key={evt.id}>
        <td className="events-cell events-date" data-label="Date">
          <span>{eventDate[2]}</span> {eventDate[1]} {eventDate[3]}
          <p>
            {startTime}
            {" - "}
            {endTime}
          </p>
        </td>
        <td className="events-cell events-description" data-label="Description">
          <h3>{evt.subject}</h3>
          <p>{evt.description}</p>
          <p>Created By {evt.user}</p>
        </td>
        <td className="events-cell events-btn" data-label="Action">
          <button
            onClick={() => editEvent(evt)}
            className="cta cta-edit"
            title="Edit Event"
          >
            Edit
          </button>
          <button
            onClick={() => deleteEvent(evt)}
            className="cta"
            title="Delete Event"
          >
            Delete
          </button>
        </td>
      </tr>
    );
  };
  return (
    <main className="main">
      <table className="events-table">
        <thead className="events-headings">
          <tr className="events-row">
            <th>Date</th>
            <th>Description</th>
            <th>Availability</th>
          </tr>
        </thead>
        <tbody>
          {eventResponse &&
            eventResponse.todayEvents &&
            eventResponse.todayEvents.length > 0 && (
              <>
                <tr>
                  <td colSpan={3}>
                    <h4>Today's Events</h4>
                  </td>
                </tr>
                {eventResponse.todayEvents.map((evt: any) => eventDetails(evt))}
              </>
            )}
          {eventResponse &&
            eventResponse.upcomingEvents &&
            eventResponse.upcomingEvents.length > 0 && (
              <>
                <tr>
                  <td colSpan={3}>
                    <h4>Upcoming Events</h4>
                  </td>
                </tr>
                {eventResponse.upcomingEvents.map((evt: any) =>
                  eventDetails(evt)
                )}
              </>
            )}
          {(!eventResponse ||
            !eventResponse.todayEvents ||
            eventResponse.todayEvents.length === 0) &&
            (!eventResponse ||
              !eventResponse.upcomingEvents ||
              eventResponse.upcomingEvents.length === 0) && (
              <>
                <tr>
                  <td colSpan={3}>
                    <h4>No Events Found.</h4>
                  </td>
                </tr>
              </>
            )}
        </tbody>
      </table>
    </main>
  );
}

export default EventList;
