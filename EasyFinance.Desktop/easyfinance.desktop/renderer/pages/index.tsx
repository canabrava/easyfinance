import { useEffect, useState } from 'react';
import Router from 'next/router';
import Login from './register/login';
// Importe seu componente de Login ou qualquer outra dependência aqui

const Home = () => {
  const [isChecking, setIsChecking] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem('token'); // Ou sua lógica para obter o token
    if (token) {
      // Verifique se o token é válido (possivelmente fazendo uma requisição para o backend)
      // Se válido, redirecione para a dashboard ou página principal
      Router.push('/dashboard'); // Substitua '/dashboard' pela rota desejada
    } else {
      setIsChecking(false); // Não há token, mostre a tela de login
    }
  }, []);

  if (isChecking) {
    return <div>Carregando...</div>; // Ou algum componente de carregamento
  }

  // Renderize o componente de Login ou a lógica da página de login aqui
  return <Login />
};

export default Home;
