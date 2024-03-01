import { useRef } from 'react';
import Link from 'next/link';
import { login } from '../../services/registerService/registerService';
import { setUser } from "../../signals/userSignal"
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import React from 'react';

const Login = () => {
  const emailRef = useRef();
  const passwordRef = useRef();

  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

      try {
        const userData = await login(
          (emailRef.current as HTMLInputElement).value, 
          (passwordRef.current as HTMLInputElement).value);

        setUser(userData);

        navigateToHomePage();
      } catch (error) {
        if (error instanceof Error) {
          toast.error(error.message);
        }
        else {
          toast.error('Ocorreu um erro desconhecido');
        }
    }
  };

  const navigateToHomePage = () => {
    router.push('/home/page');
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen">
      <div className="px-8 pt-6 pb-8 mb-4 bg-white border border-gray-300 rounded-3xl">
        <div>
          <img src="/images/easyfinance-logo.png" alt="Logo" className="w-32 h-auto mx-auto" />
        </div>
        <form onSubmit={handleSubmit} className="px-8 pt-6 pb-8 mb-4 bg-white">
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
          <div className="mb-6">
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
          <div className="flex items-center justify-between">
              <button type="submit" className="btn-primary">
                  Logar
              </button>
              <div  className="link-primary">
                <Link href="/register/register">
                    Novo Usuário
                </Link>
              </div>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
