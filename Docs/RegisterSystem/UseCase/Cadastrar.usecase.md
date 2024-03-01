# Cadastro de Usuário no EasyFinance

## Identificador: UC-001

### Ator Principal
Usuário Final

### Resumo
Este caso de uso descreve o processo pelo qual um novo usuário se cadastra no sistema EasyFinance.

### Pré-condições
Nenhuma.

### Fluxo Principal
1. O usuário acessa a página de cadastro.
2. O usuário insere seu nome, e-mail e senha.
3. O sistema valida as informações inseridas.
4. O sistema faz a criptografia da senha.
5. O sistema cria uma nova conta de usuário e gera um ID único (Guid).
6. O sistema exibe uma mensagem de sucesso e envia um e-mail de confirmação.

### Fluxos Alternativos

**E-mail já cadastrado:** 
- Se o e-mail já estiver cadastrado, o sistema exibe uma mensagem de erro e solicita que o usuário tente um e-mail diferente.

**Dados inválidos:** 
- Se os dados inseridos forem inválidos, o sistema exibe uma mensagem de erro e solicita a correção dos dados.

### Pós-condições
- Em caso de sucesso, uma nova conta de usuário é criada.
- Em caso de falha, nenhuma nova conta é criada.