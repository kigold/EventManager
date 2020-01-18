import axios, {handleResponse, displayError} from "./index.service";
import config from "../config/config";
import { addTokenToHeader } from "../utils";

const { pageSize } = config;

export default class EventService {
    static getEvents(pageNumber, keyword, month) {
      return axios.get(
          `/events?pageNumber=${pageNumber}&pageSize=${pageSize}&keyword=${keyword}&month=${month}`
      );
    }

    static getEvent(id) {
        return axios.get(`/events/${id}`);
    }

    static createEvent(payload) {
        return axios.post(`/events`, payload);
    }

    static editEvent(payload) {
        return axios.put(`/events`, payload);
    }

}
export const queryEvents = async (pageNumber, keyword, month) => {
  //addTokenToHeader();
  try {
       console.log("making api call")
      const events = await EventService.getEvents(pageNumber, keyword, month)
      return handleResponse(events);
  } catch (error) {
    displayError(error);
  }
};