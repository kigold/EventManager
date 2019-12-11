import axios from "axios";
import config from "../config/config";

const { apiBaseUrl } = config;

const instance = axios.create({
  baseURL: apiBaseUrl,
  withCredentials: false
  /*headers: {
    "Content-Type": "application/json;charset=utf-8"
  }*/
});

export default instance;

export const handleResponse = (payload) => {
  try {
    console.log('handling response')
        if(payload.data.code == 1){
          //Success
          console.log(payload)
          return payload.data.payload
        }
        else{
          return displayError(payload)
        }

  } catch (error) {
    
  }
};

export const displayError = (payload) => {
  try {       
    //display Error
    console.log(payload);
    alert("An error occured");

  } catch (error) {    
    alert("An error occured, ..",error);
  }
  return null;
};
