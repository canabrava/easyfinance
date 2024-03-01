import axios from 'axios';
import ApiErrorProcessor from '../../utils/apiErrorProcessor';

const API_URL = process.env.NEXT_PUBLIC_REGISTER_SYSTEM_API_URL;

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  },
});

export const login = async (email, password) => {
  try {
    const response = await api.post('/api/user/login', { email, password });

    return response.data;
  } catch (error) {
    console.log(error);
    const formattedError = await ApiErrorProcessor.processError(error.response);

    throw new Error(formattedError);
  }
}

export const register = async (name, email, password) => {
  try {
    const response = await api.post('/api/user/register', { name, email, password });

    return response.data;
  } catch (error) {
    const formattedError = await ApiErrorProcessor.processError(error.response);

    throw new Error(formattedError);
  }
}