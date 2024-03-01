// jest.config.js
module.exports = {
  testEnvironment: 'jsdom',
  transform: {
    '^.+\\.(ts|tsx|js|jsx)$': 'babel-jest',
  },
  transformIgnorePatterns: [
    "node_modules/(?!(jest-)?react-native|react-clone-referenced-element|@react-native-community|@react-navigation|@unimodules|unimodules|expo(nent)?|@expo(nent)?/.*|react-navigation|@react-navigation/.*|@unimodules/.*|unimodules|expo-.+|@expo/.+|@preact/signals-react/.*\\.js$)"
  ],
  moduleNameMapper: {
    // Seus mapeamentos aqui
  },
  setupFilesAfterEnv: [
    '<rootDir>/jest.setup.js', // aponta para o arquivo de setup do Jest
  ],
  // Outras configurações podem ser adicionadas conforme necessário
};