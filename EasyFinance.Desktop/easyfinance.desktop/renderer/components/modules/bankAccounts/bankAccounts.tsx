import React, { useEffect, useState } from 'react';
import AddButton from '../../ui/addButtonComponent/addButton';
import Accordion from '../../ui/accordionComponent/accordion';
import LoadingLogo from '../../ui/loadingLogoComponent/loadingLogo';
import {getBankAccountsSignal, setBankAccountsSignal} from '../../../signals/bankAccountsSignal'
import AddBankAccountForm from './addBankAccountForm'
import SplitBoard from '../../ui/splitBoardComponent/splitBoard';


const BankAccounts = () => {
    const [bankAccounts, setBankAccounts] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [showAddForm, setShowAddForm] = useState(false);
    const [showMaskedBalance, setShowMaskedBalance] = useState(true);


    const handleAddClick = () => {
        setShowAddForm(true);
    };

    const handleCloseModal = () => {
        setShowAddForm(false);
    };

    useEffect(() => {
        getAccounts();
    }, []);

    function getAccounts() {
        if(getBankAccountsSignal().length === 0){
            setIsLoading(true);
        }
        else{
            setBankAccounts(getBankAccountsSignal());
        }

        
            const fetchedAccounts = [
                { id: '1', name: 'Conta Corrente', number: '1234-5', balance: 'R$ 1.200,00' },
                { id: '2', name: 'Conta Poupança', number: '6789-0', balance: 'R$ 3.400,00' }
            ];

            setBankAccounts(fetchedAccounts);
            setIsLoading(false);
            setBankAccountsSignal(fetchedAccounts)
        }
    

    return (
        <div className="w-full h-full py-10 bg-tertiary">
            <div className="container px-4 mx-auto">
            <div className="flex items-center justify-center mb-6">
                <h1 className="text-title">Suas Contas Bancárias</h1>
            </div>
                <div className="flex items-center justify-between mb-6">
                    <AddButton onClick={handleAddClick} />
                    <div className="relative flex items-center">
                        <span className={`text-lg mr-2 ${!showMaskedBalance ? 'text-secondary' : 'text-gray-500'}`}>$</span>
                        <label className="relative inline-flex items-center cursor-pointer">
                            <input 
                                type="checkbox" 
                                className="sr-only peer" 
                                checked={!showMaskedBalance}
                                onChange={() => setShowMaskedBalance(!showMaskedBalance)}
                            />
                            <div className="w-11 h-6 bg-gray-200 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full rtl:peer-checked:after:-translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-0.5 after:start-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-secondary"></div>
                        </label>
                    </div>
                </div>

                {showAddForm && <AddBankAccountForm onClose={handleCloseModal} />}
    
                {isLoading ? (
                    <LoadingLogo />
                ) : (
                    <div className="px-8 pt-6 pb-8 mb-4 bg-white rounded shadow-md">
                    {bankAccounts.map((account, index) => (
                        <Accordion title={account.name} key={account.id}>
                            <SplitBoard
                                id={'BankAccount' + account.id}
                                leftChild={<LoadingLogo />}
                                rightChild={<LoadingLogo />}
                                />            
                        </Accordion>
                    ))}
                </div>
                )}
            </div>
        </div>
    );
};

export default BankAccounts;
