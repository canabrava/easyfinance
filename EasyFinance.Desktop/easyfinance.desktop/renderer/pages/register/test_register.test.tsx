import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import Register from './register';
import { waitFor } from '@testing-library/react';
import { register } from '../../services/registerService/registerService';
import { toast } from 'react-toastify';

jest.mock('react-toastify', () => ({
  toast: {
    error: jest.fn(),
    success: jest.fn(),
  },
}));

jest.mock('../../services/registerService/registerService', () => ({
  register: jest.fn(),
}));

describe('Register', () => {
  test('renders register form', () => {
    render(<Register />);
    
    // Assert that the form elements are rendered


    expect.extend({ // Add the toBeInTheDocument function to the type definition
      toBeInTheDocument(element: HTMLElement) {
        const pass = element !== null;
        if (pass) {
          return {
            message: () => `expected element not to be in the document`,
            pass: true,
          };
        } else {
          return {
            message: () => `expected element to be in the document`,
            pass: false,
          };
        }
      },
    });

    // @ts-ignore
    expect(screen.getByLabelText('Nome:')).toBeInTheDocument();
    // @ts-ignore
    expect(screen.getByLabelText('E-mail:')).toBeInTheDocument();
    // @ts-ignore
    expect(screen.getByLabelText('Senha:')).toBeInTheDocument();
    // @ts-ignore
    expect(screen.getByLabelText('Confirme a Senha:')).toBeInTheDocument();
    // @ts-ignore
    expect(screen.getByRole('button', { name: 'Registrar' })).toBeInTheDocument();
    // @ts-ignore
    expect(screen.getByRole('link', { name: 'Já tem uma conta? Login' })).toBeInTheDocument();
  });

  test('submits form with valid data', async () => {
    render(<Register />);
    
    // Fill in the form fields
    fireEvent.change(screen.getByLabelText('Nome:'), { target: { value: 'Test User' } });
    fireEvent.change(screen.getByLabelText('E-mail:'), { target: { value: 'test@example.com' } });
    fireEvent.change(screen.getByLabelText('Senha:'), { target: { value: 'password' } });
    fireEvent.change(screen.getByLabelText('Confirme a Senha:'), { target: { value: 'password' } });
  
    // Submit the form
    fireEvent.click(screen.getByRole('button', { name: 'Registrar' }));
    
    // Wait for the register function to resolve and the success toast to be displayed
    await waitFor(() => expect(toast.success).toHaveBeenCalledWith('Registro realizado com sucesso!'));
  });

  test('displays error message for password mismatch', async () => {
    render(<Register />);
    
    // Fill in the form fields with mismatched passwords
    fireEvent.change(screen.getByLabelText('Nome:'), { target: { value: 'John Doe' } });
    fireEvent.change(screen.getByLabelText('E-mail:'), { target: { value: 'john@example.com' } });
    fireEvent.change(screen.getByLabelText('Senha:'), { target: { value: 'password' } });
    fireEvent.change(screen.getByLabelText('Confirme a Senha:'), { target: { value: 'differentpassword' } });
    
    // Submit the form
    fireEvent.click(screen.getByRole('button', { name: 'Registrar' }));
    
    // Assert that the error message is displayed
    expect(toast.error).toHaveBeenCalledWith('As senhas não coincidem.');
  });
});