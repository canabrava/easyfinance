// components/Footer.js

import React from 'react';

const FooterComponent = () => {
    return (
        <footer className="bottom-0 w-full h-full p-4 text-xs text-center bg-gray-200 max-h-950:hidden">
            <p>Feito com <span className="text-red-500">&hearts;</span> por Canabrava</p>
            <p>Siga-nos em:
                <a href="https://github.com/canabrava" target="_blank" rel="noopener noreferrer" className="mx-2 text-blue-500 hover:underline">Github</a>
            </p>
        </footer>
    );
};

export default FooterComponent;
