import { useRef } from 'react';
import Link from 'next/link';
import { register } from '../../services/registerService/registerService';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import React from 'react';

const Register = () => {
  const nameRef = useRef();
  const emailRef = useRef();
  const passwordRef = useRef();
  const comfirmPasswordRef = useRef();


  const router = useRouter();


  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const password = (passwordRef.current as HTMLInputElement).value;
    const confirmPassword = (comfirmPasswordRef.current as HTMLInputElement).value;

    if (password !== confirmPassword) {
      toast.error('As senhas não coincidem.');
      return;
    }

    try {
      const userData = await register(
        (nameRef.current as HTMLInputElement).value,
        (emailRef.current as HTMLInputElement).value, 
        password);
      
      toast.success('Registro realizado com sucesso!');
      router.push('login');
    } catch (error) {
      if (error instanceof Error) {
        toast.error(<div dangerouslySetInnerHTML={{__html: error.message}} />);
      } else {
        toast.error('Ocorreu um erro desconhecido');
      }
    }
  };
  

  return (
    <div className="flex flex-col items-center justify-center min-h-screen">
      <form onSubmit={handleSubmit} className="px-20 pt-20 pb-20 mb-4 bg-white shadow-md rounded-3xl">
        <div className="mb-4">
          <label htmlFor="name" className="form-text">
            Nome:
          </label>
          <input
            type="text"
            id="name"
            ref={nameRef}
            className="form-input"
            required
          />
        </div>
        <div className="mb-4">
          <label htmlFor="email" className="form-text">
            E-mail:
          </label>
          <input
            type="email"
            id="email"
            ref={emailRef}
            className="form-input"
            required
          />
        </div>
        <div className="mb-4">
          <label htmlFor="password" className="form-text">
            Senha:
          </label>
          <input
            type="password"
            id="password"
            ref={passwordRef}
            className="form-input"
            required
          />
        </div>
        <div className="mb-6">
          <label htmlFor="confirmPassword" className="form-text">
            Confirme a Senha:
          </label>
          <input
            type="password"
            id="confirmPassword"
            ref={comfirmPasswordRef}
            className="form-input"
            required
          />
        </div>
        <div className="flex items-center justify-between space-x-4">
          <button type="submit" className="btn-primary">
            Registrar
          </button>
          <div className="link-primary">
          <Link href="login" className="inline-block text-sm font-bold text-blue-500 align-baseline hover:text-blue-800">
            Já tem uma conta? Login
          </Link>
          </div>
        </div>
      </form>
    </div>
  );
};

export default Register; 
