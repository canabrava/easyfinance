# Autenticação de Usuário no EasyFinance

## Identificador: UC-002

### Ator Principal
Usuário Final

### Resumo
Este caso de uso descreve o processo pelo qual um usuário se autentica no sistema EasyFinance para acessar suas funcionalidades.

### Pré-condições
- O usuário deve ter uma conta já cadastrada no sistema.

### Fluxo Principal
1. O usuário acessa a página de login.
2. O usuário insere seu e-mail e senha.
3. O sistema valida as informações inseridas.
4. Se as informações estiverem corretas, o sistema concede acesso ao usuário.
5. O usuário é redirecionado para a página inicial do sistema.

### Fluxos Alternativos

**Dados Incorretos:**
- Se o e-mail ou a senha estiverem incorretos, o sistema exibe uma mensagem de erro e solicita que o usuário tente novamente.

### Pós-condições
- Em caso de sucesso, o usuário obtém acesso às funcionalidades do sistema.
- Em caso de falha, o acesso é negado.
