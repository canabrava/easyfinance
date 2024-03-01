// tailwind.config.js

module.exports = {
  content: [
    './src/**/*.{js,ts,jsx,tsx}', // Caminhos para arquivos onde o Tailwind será aplicado
    './renderer/pages/**/*.{js,ts,jsx,tsx}', // Páginas do Next.js
    './renderer/components/**/*.{js,ts,jsx,tsx}', // Componentes do Next.js
    './renderer/modules/**/*.{js,ts,jsx,tsx}' // Modulos do Next.js
  ],
  theme: {
    extend: {
      fontFamily: {
        // Exemplo de configuração de fontes personalizadas
        sans: ['Inter', 'Arial', 'sans'],
        screens: {
          'max-h-950': {'max': '950px'}
        }
      },
      colors: {
        // Exemplo de configuração de cores personalizadas
        primary: '#092C3F',
        secondary: '#00C6B1',
        tertiary: '#F1F1E6',
        darktertiary: '#B5B5A5'
      },
      boxShadow: {
        'outline': '0 0 0 3px rgba(66, 153, 225, 0.5)', // Defina o valor da sombra aqui
      },
      // Adicione outras configurações de tema personalizadas conforme necessário
    },
  },
  plugins: [
    // Exemplo de uso de plugins do Tailwind
    require('@tailwindcss/forms'), // Para estilização de formulários
    require('@tailwindcss/typography'), // Para estilização de tipografia
    // Adicione outros plugins conforme necessário
  ],
};
