import React from 'react';
import { getUser } from '../../../signals/userSignal'
import styles from './headerComponent.module.css';


const HeaderComponent = () => {
  // Função para determinar a saudação (Bom dia, Boa tarde, Boa noite)
  const getSaudacao = () => {
    const user = getUser();

    const hora = new Date().getHours();
    if (hora < 12) return 'Bom dia, ';
    if (hora < 18) return 'Boa tarde, ';
    return 'Boa noite, ';
  };

  return (
    <div className="flex items-center justify-between header-container">
      <div className={styles['image-background']}>
        <img src="/images/easyfinance-logo.png" alt="Logo" className="w-24 h-auto mx-auto" />
      </div>

      <h1 className={styles['greeting-text']}>{getSaudacao()} {getUser().name}</h1>
      
      <div className={styles['image-background']}>
        <img src="/images/user-default.png" alt="Profile" className="w-24 h-auto mx-auto rounded-full" />
      </div>
    </div>
  );
};

export default HeaderComponent;
