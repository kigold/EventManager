import axios, {handleResponse, displayError} from "./index.service";
import config from "../config/config";
import { addTokenToHeader } from "../utils";

const { pageSize } = config;

export default class EventService {
    static getEvents1(pageNumber, keyword) {
      return axios.get(
        `/events?pageNumber=${pageNumber}&pageSize=${pageSize}&keyword=${keyword}`
      );
    }

    static getEvent(keyword) {
        return axios.get(`/events/${keyword}`);
    }

    static createEvent(payload) {
        return axios.post(`/events`, payload);
    }

    static editEvent(payload) {
        return axios.put(`/events`, payload);
    }

}
export const getEvents = async (pageNumber, keyword) => {
  //addTokenToHeader();
  try {
       console.log("making api call")
      const events = await EventService.getEvents1(pageNumber, keyword)
      return handleResponse(events);
  } catch (error) {
    displayError(error);
  }
};