import React, { useEffect, useState } from 'react';
import LoadingLogo from '../../ui/loadingLogoComponent/loadingLogo';
import styles from './tabsComponent.module.css'
import BankAccounts from '../../modules/bankAccounts/bankAccounts'

const TabsComponent = () => {
    const [activeTab, setActiveTab] = useState('Bancos');

    const [isSmallScreen, setIsSmallScreen] = useState(window.innerHeight < 600);

    useEffect(() => {
        const handleResize = () => {
            setIsSmallScreen(window.innerHeight <= 600);
        };

        window.addEventListener('resize', handleResize);

        return () => {
            window.removeEventListener('resize', handleResize);
        };
    }, []);


    const TabContent = () => {
        switch(activeTab) {
            case 'Bancos':
                return <BankAccounts />;
            case 'Cartões':
                return <LoadingLogo />;
            case 'Despesas/Receitas':
                return <LoadingLogo />;
            case 'Investimentos':
                return <LoadingLogo />;
            default:
                return null;
        }
    };

    return (
<div className={styles.mainBoard}>
    <ul className={styles.selectTabs}>
        {['Bancos', 'Cartões', 'Despesas/Receitas', 'Investimentos'].map((tab, index, arr) => (
            <li key={tab} className="flex-1 first:ml-0 last:mr-0">
                <button
            className={`block w-full h-full py-2 px-4 text-2xl font-medium text-center transition-all duration-300 
                        ${activeTab === tab 
                            ? 'tab-selected'
                            : 'tab-primary'} focus:outline-none
                        ${index === 0 && !isSmallScreen ? 'rounded-tl-full' : ''}
                        ${index === arr.length - 1 && !isSmallScreen ? 'rounded-tr-full' : ''}`}
            onClick={() => setActiveTab(tab)}
        >
            {tab}
        </button>
            </li>
        ))}
    </ul>
    <div className="flex items-center justify-center flex-1 scroll-container">
        <TabContent />
    </div>
</div>

    );
};

export default TabsComponent;
