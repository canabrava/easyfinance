import React, { useEffect } from 'react';

const AddBankAccountForm = ({ onClose }) => {

      useEffect(() => {
        const handleKeyDown = (event) => {
            if (event.key === 'Escape') {
                onClose();
            }
        };
      
        window.addEventListener('keydown', handleKeyDown);
        return () => {
            window.removeEventListener('keydown', handleKeyDown);
        };
    }, [onClose]);

    const submitForm = (e) => {
        e.preventDefault();
        console.log("Formulário submetido!");
        onClose();
    };

    return (
        <dialog open className="relative z-50" aria-modal="true">
            <div className="fixed inset-0 bg-black bg-opacity-50" onClick={onClose}></div>
        
            <div className="fixed inset-0 flex items-center justify-center p-4">
              <form onSubmit={submitForm} className="max-h-screen px-20 pt-20 pb-20 mb-4 overflow-auto bg-white shadow-md rounded-3xl">
                <h2 className="text-title">Adicionar Conta Bancária</h2>
                
                <br/>
                
                <div className="mb-4">
                  <label className="form-text">
                    Nome da conta:
                    <input type="text" name="name" required className="form-input" />
                  </label>
                </div>

                <div className="mb-4">
                  <label className="form-text">
                    Código do banco:
                    <input type="text" name="number" required className="form-input" />
                  </label>
                </div>

                <div className="mb-4">
                  <label className="form-text">
                    Número:
                    <input type="text" name="number" required className="form-input" />
                  </label>
                </div>

                <div className="mb-4">
                  <label className="form-text">
                    Agência:
                    <input type="text" name="number" required className="form-input" />
                  </label>
                </div>

                <div className="mb-4">
                  <label className="form-text">
                    Tipo de Conta:
                    <input type="text" name="number" required className="form-input" />
                  </label>
                </div>

                <div className="mb-6">
                  <label className="form-text">
                    Saldo:
                    <input type="text" name="balance" required className="form-input" />
                  </label>
                </div>

                <div className="flex items-center justify-between">
                  <button type="submit" className="btn-primary">
                    Adicionar
                  </button>
                  <button type="button" onClick={onClose} className="btn-cancel">
                    Cancelar
                  </button>
                </div>
              </form>
            </div>
        </dialog>
    );
};

export default AddBankAccountForm;
