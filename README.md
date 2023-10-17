# Implementação das arquiteturas para o TCC
## Título do TCC: Arquitetura de software: Desempenho x Manutenção

### Para rodar o projeto utilize:

Sem arquitetura

docker-compose -f noarch-compose.yaml up -d

Arquitetura limpa

docker-compose -f cleanarch-compose.yaml up -d

Hexagonal

docker-compose -f hexagonalarch-compose.yaml up -d

### Serão implementadas as seguintes funcionalidades para cada arquitetura:
1. Login ✔
2. Registro ✔
3. Importação de excel ✔
4. Persistência assincrona dos dados em banco de dados ✔
5. Salvamento do arquivo em disco para download do usuário ✔
6. Gerar logs das ações executadas e status dos processos ✔
 
### As seguintes modificações serão feitas para a geração das métricas de manutenibilidade:
1. Troca de gerencimento de banco de dados (dynamodb -> postgres) ✔
2. Troca de gerenciamento de arquivos (disco -> nuvem) ✔
3. Troca de gerenciamento de logs (console -> arquivo) ✔
4. Troca de gerenciamento de planilha (? -> ?) ✔
5. Adição de novas propriedades no modelo do usuário (created_at, updated_at) ✔
6. Adição de nova propriedade na tabela, que tornará possível a troca do nome do produto ✔

### Regras da importação:
1. Caso um produto exista no banco e não esteja na tabela, deverá ser removido.
2. Caso um produto não exista no banco e esteja na tabela, deverá ser adicionado.
3. Caso um produto exista no banco e esteja na tabela, deverá ser atualizado.

### O arquivo a ser importado conterá os seguintes campos:
- (string) nome
- (string) descrição
- (decimal) valor
- (int) quantidade em estoque

### O usuário conterá os seguintes campos:
-> Nome da tabela: Usuarios
- (int) id
- (string) nome
- (string) username
- (string) senha

### O produto terá os seguintes campos:
-> Nome da tabela: Produtos
- (int) id
- (string) nome
- (string) descrição
- (decimal) valor
- (int) quantidade em estoque
- (int) donoId -> id do usuario que executou a ação