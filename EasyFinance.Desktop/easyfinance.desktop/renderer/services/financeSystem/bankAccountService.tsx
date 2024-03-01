import axios from 'axios';
import { useMutation, useQuery } from 'react-query';
import { getAuthorizationHeader } from '../../services/authService';
import ApiErrorProcessor from '../../utils/apiErrorProcessor';

const API_URL = 'http://your-api-url.com';

const api = axios.create({
    baseURL: API_URL,
    headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    },
  });

const createBankAccount = async (data) => {
  try {
    const response = await api.post(`${API_URL}/api/bankaccount/create`, data, {
      headers: getAuthorizationHeader(),
    });
    return response.data;
  } catch (error) {
    throw new Error(ApiErrorProcessor.processError(error));
  }
};

const getBankAccounts = async () => {
  try {
    const response = await api.get(`${API_URL}/api/bankaccount/`, {
      headers: getAuthorizationHeader(),
    });
    return response.data;
  } catch (error) {
    throw new Error(ApiErrorProcessor.processError(error));
  }
};

const updateBankAccount = async ({ bankAccountId, data }) => {
  try {
    await api.put(`${API_URL}/api/bankaccount/${bankAccountId}`, data, {
      headers: getAuthorizationHeader(),
    });
  } catch (error) {
    throw new Error(ApiErrorProcessor.processError(error));
  }
};

const deleteBankAccount = async (bankAccountId) => {
  try {
    await api.delete(`${API_URL}/api/bankaccount/${bankAccountId}`, {
    headers: getAuthorizationHeader(),
    });
  } catch (error) {
    throw new Error(ApiErrorProcessor.processError(error));
  }
};

export const useCreateBankAccount = () => useMutation(createBankAccount);
export const useGetBankAccounts = () => useQuery('bankAccounts', getBankAccounts);
export const useUpdateBankAccount = () => useMutation(updateBankAccount);
export const useDeleteBankAccount = () => useMutation(deleteBankAccount);