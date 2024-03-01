import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { login } from '../../services/registerService/registerService';
import { setUser } from '../../signals/userSignal';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import Login from './login';

jest.mock('../../services/registerService/registerService', () => ({
  login: jest.fn(),
}));

jest.mock('../../signals/userSignal', () => ({
  setUser: jest.fn(),
}));

jest.mock('react-toastify', () => ({
  toast: {
    error: jest.fn(),
  },
}));

jest.mock('next/router', () => ({
  useRouter: jest.fn(),
}));

describe('Login', () => {
  test('submits form with valid data', async () => {
    const routerPushMock = jest.fn();
    (useRouter as jest.Mock).mockReturnValue({ push: routerPushMock });

    render(<Login />);

    const emailInput = screen.getByLabelText('E-mail:');
    const passwordInput = screen.getByLabelText('Senha:');
    const submitButton = screen.getByRole('button', { name: 'Logar' });

    // Fill in the form fields
    fireEvent.change(emailInput, { target: { value: 'test@example.com' } });
    fireEvent.change(passwordInput, { target: { value: 'password' } });

    // Mock the login function to return user data
    (login as jest.Mock).mockResolvedValue({ name: 'Test User' });

    // Submit the form
    fireEvent.click(submitButton);

    // Wait for the login function to resolve and the user to be set
    await waitFor(() => expect(setUser).toHaveBeenCalledWith({ name: 'Test User' }));

    // Check that the user is redirected to the home page
    expect(routerPushMock).toHaveBeenCalledWith('/home/page');
  });

  test('displays error message for login failure', async () => {
    render(<Login />);

    const emailInput = screen.getByLabelText('E-mail:');
    const passwordInput = screen.getByLabelText('Senha:');
    const submitButton = screen.getByRole('button', { name: 'Logar' });

    // Fill in the form fields
    fireEvent.change(emailInput, { target: { value: 'test@example.com' } });
    fireEvent.change(passwordInput, { target: { value: 'password' } });

    // Mock the login function to throw an error
    (login as jest.Mock).mockRejectedValue(new Error('Invalid credentials'));

    // Submit the form
    fireEvent.click(submitButton);

    // Wait for the error toast to be displayed
    await waitFor(() => expect(toast.error).toHaveBeenCalledWith('Invalid credentials'));
  });
});