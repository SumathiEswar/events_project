import React, { useState } from "react";
import { CreateNewEvent, UpdateEvent } from "../../constants/rest-api-constant";
import { FetchApiPromise } from "../../services/api-service";
import "./CreateEvent.css";

function CreateEvent({ editEvent, tabChange }: any) {
  const [setEventResponse] = useState([] as any);
  const [inputs, setInputs] = useState(
    editEvent && editEvent.subject
      ? editEvent
      : {
          user: "",
          date: "",
          startTime: "",
          endTime: "",
          subject: "",
          description: "",
        }
  );

  const handleChange = (event: any) => {
    const name = event.target.name;
    const value = event.target.value;
    setInputs((values: any) => ({ ...values, [name]: value }));
  };

  const handleSubmit = (event: any) => {
    event.preventDefault();
    if (
      inputs &&
      inputs.user &&
      inputs.subject &&
      inputs.date &&
      inputs.startTime &&
      inputs.endTime
    ) {
      if (editEvent && editEvent.subject) {
        updateEvent();
      } else {
        createEvent();
      }
    } else {
      alert("Please fill the form to continue.");
    }
  };
  const createEvent = () => {
    FetchApiPromise(CreateNewEvent, "POST", JSON.stringify(inputs))
      .then((response: any) => response.json())
      .then((data) => {
        if (data && data.isSuccess) {
          tabChange();
        }
      })
      .catch((error) => {
        // Handle any errors
        setEventResponse(error);
        console.error("Error:", error);
      });
  };
  const updateEvent = () => {
    FetchApiPromise(UpdateEvent, "PUT", JSON.stringify(inputs))
      .then((response: any) => response.json())
      .then((data) => {
        if (data && data.isSuccess) {
          tabChange();
        }
      })
      .catch((error) => {
        // Handle any errors
        setEventResponse(error);
        console.error("Error:", error);
      });
  };
  return (
    <main className="main">
      <form className="form" onSubmit={handleSubmit}>
        <div className="form-group">
          <label className="form-input-label">Enter your name *</label>
          <input
            type="text"
            name="user"
            className="form-input"
            value={inputs.user || ""}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-input-label">Enter Event Name *</label>
          <input
            type="text"
            name="subject"
            className="form-input"
            value={inputs.subject || ""}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-input-label">Enter Event Description *</label>
          <input
            type="text"
            name="description"
            className="form-input"
            value={inputs.description || ""}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-input-label">Event Date *</label>
          <input
            type="date"
            name="date"
            className="form-input"
            min={new Date().toISOString().split("T")[0]}
            value={inputs.date || ""}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-input-label">Event Start Time *</label>
          <input
            type="time"
            name="startTime"
            className="form-input"
            value={inputs.startTime || ""}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-input-label">Event End Time *</label>
          <input
            type="time"
            name="endTime"
            className="form-input"
            value={inputs.endTime || ""}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <button className="cancel-btn" type="button" onClick={()=> tabChange()}>
            Cancel
          </button>
          <button className="submit-btn" type="submit">
            {editEvent && editEvent.subject ? "Update" : "Create"}
          </button>
        </div>
      </form>
    </main>
  );
}

export default CreateEvent;
