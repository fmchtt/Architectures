# Resultados dos testes das arquiteturas

## Testes de carga

( analise dos dados )

| Arquitetura | Regra | CPU 
| --- | --- | --- |
| Clean Arch | Registro | 59.78% |
| Clean Arch | Login | 21.31% |
| Clean Arch | Importação | 86.46% |
| Clean Arch | Retorno Lista | 90.00% |
| --- | --- | --- | --- |
| Hexagonal Arch | Registro | 22.93% |
| Hexagonal Arch | Login | 7.42% |
| Hexagonal Arch | Importação | ~25.53% |
| Hexagonal Arch | Retorno Lista | 68,53% |
| --- | --- | --- | --- |
| No Arch | Registro | 61.80% |
| No Arch | Login | 30.03% |
| No Arch | Importação | ~41.55% |
| No Arch | Retorno Lista | 70.77% |

## Testes de desempenho

( analise do desempenho )

| Arquitetura | Regra | Requisições | p95 | p90 | max | min | avg | iterations |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Clean Arch | Registro | 5000 | 67 ms | 55.99 ms | 727.52 ms | 6.99 ms | 31 ms | 1223.86/s |
| Clean Arch | Login | 5000 | 43.05 ms | 32 ms | 305.59 ms | 2.99 ms | 16 ms | 2277.59/s |
| Clean Arch | Importação | 5000 | 1.77 s | 1.57 s | 3.87 s | 2.51 ms | 329.11 ms | 31.4/s |
| Clean Arch | Retorno Lista | 5000 | 335.29 ms | 281.52 ms | 702.03 ms | 1.99 ms | 131.06 ms | 186.44/s |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Hexagonal Arch | Registro | 5000 | 71ms | 57.32ms | 665.02ms | 5.98ms | 38.34ms | 1292.75/s |
| Hexagonal Arch | Login | 5000 | 50.33ms | 39.99ms | 277.36ms | 2.99ms | 23.91ms | 1976.99/s |
| Hexagonal Arch | Importação | 3354 (67%) | 17.91s | 13.23s | 48.89s | 7.91ms | 4.46s | 5.4/s |
| Hexagonal Arch | Retorno Lista | 3354 | 305.88ms | 245.25ms | 1.99ms | 775.4ms | 91.01ms | 238.97/s |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| No Arch | Registro | 5000 | 79.52ms | 63ms | 739.41ms | 3.99ms | 41.11ms | 1205.45/s |
| No Arch | Login | 5000 | 45ms | 34ms | 239.67ms | 2.99ms | 21.19ms | 2322.82/s |
| No Arch | Importação | 3621 (72%) | 11.52s | 9.43s | 20.94s | 5.78ms | 4.09s | 5.96/s |
| No Arch | Retorno Lista | 3621 | 184.76ms | 156.96ms | 437.32ms | 1.99ms | 67.84ms | 292.19/s |

## Manutenção

#### Ferimentos dos principios solid:

( analise de ferimentos )

#### Tempo gasto em desenvolvimento:

( analise de tempo )

#### Quantidade de arquivos:

( analise de arquivos gerados )

#### M�tricas de complexidade

| Arquitetura | Complexidade Ciclomatica | Complexidade Coginitiva |
| --- | --- | --- |
| CleanArch | 244 | 0 | 
| NoArch | 139 | 24 |
| HexagonalArch | X | X |