import React, { useState } from "react";
import "./App.css";
import CreateEvent from "./components/create-event/CreateEvent";
import EventList from "./components/events-list/EventList";

function App() {
  const [eventTab, setEventTab] = useState(false);
  const [editEvent, setEditEvent] = useState(null as any);
  const eventButton = (evt: any) => {
    setEditEvent(evt);
    setEventTab(true);
  };
  const eventListButton = () => {
    setEditEvent(null);
    setEventTab(false);
  };
  return (
    <div className="App">
      <header className="App-header">
        <p>Events Project</p>
      </header>
      <button
        className={"tab-btn " + (eventTab ? "tab-in-active" : "tab-active")}
        onClick={() => eventListButton()}
      >
        Event List
      </button>
      <button
        className={"tab-btn " + (eventTab ? "tab-active" : "tab-in-active")}
        onClick={() => eventButton(null)}
      >
        {editEvent && editEvent.subject ? "Update Event" : "Create Event"}
      </button>
      {!eventTab && <EventList editEvent={eventButton}></EventList>}
      {eventTab && (
        <CreateEvent
          editEvent={editEvent}
          tabChange={() => eventListButton()}
        ></CreateEvent>
      )}
    </div>
  );
}

export default App;
