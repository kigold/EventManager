import jwtDecode from "jwt-decode";
import axios from "../services/index.service";

const savedtoken = window.localStorage.getItem("token");
let decode;
export const isLoggedIn = () => {
  try {
    decode = jwtDecode(savedtoken);
  } catch (error) {
    return false;
  }

  const { auth_time } = decode;
  const currentDate = new Date();

  return auth_time * 1000 - currentDate.getTime() > 1;
};

export const decodeUser = (token = savedtoken) => {
  try {
    decode = jwtDecode(token);
    return { email: decode.email, fullName: decode.fullname };
  } catch (error) {
    return "";
  }
};

export const addTokenToHeader = () => {
  try {
    const token = localStorage.getItem("token");
    if (token != "" || token == undefined) {
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    } else {
      //TODO redirect to Login page if necessary
    }
  } catch (error) {}
};
