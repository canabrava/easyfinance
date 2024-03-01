import React from 'react';
import 'animate.css';
import styles from  './loadingLogo.module.css';

const LoadingLogo = () => {
  return (
    <div className={styles.logoAnimationContainer}>
      <img 
        src="/images/easyfinance-logo.png" 
        alt="Logo" 
        className={`${styles.logoAnimation} animate__animated animate__pulse animate__infinite`}
      />
    </div>
  );
}


export default LoadingLogo;