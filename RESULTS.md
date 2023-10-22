# Resultados dos testes das arquiteturas

## Testes de carga

( analise dos dados )

| Arquitetura | Regra | CPU 
| --- | --- | --- |
| Clean Arch | Registro | 52.17% |
| Clean Arch | Login | 56.47% |
| Clean Arch | Importação | ~62.00% |
| Clean Arch | Retorno Lista | 37.76% |
| --- | --- | --- | --- |
| Hexagonal Arch | Registro | 51.78% |
| Hexagonal Arch | Login | 61.22% |
| Hexagonal Arch | Importação | ~68.00% |
| Hexagonal Arch | Retorno Lista | 38.45% |
| --- | --- | --- | --- |
| No Arch | Registro | 54.43% |
| No Arch | Login | 63.49% |
| No Arch | Importação | ~71.00% |
| No Arch | Retorno Lista | 54.01% |

## Testes de desempenho

( analise do desempenho )

| Arquitetura | Regra | Requisições | p95 | p90 | max | min | avg | iterations |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Clean Arch | Registro | 5000 | 73.58ms | 62.17ms | 550.59ms | 2.99ms | 38.24ms | 1294.84/s |
| Clean Arch | Login | 5000 | 46.96 ms | 38.99ms | 265.06ms | 1.99ms | 22.62ms | 2180.19/s |
| Clean Arch | Importação | 5000 | 2.57s | 2.37s | 3.4s | 1.99ms | 767.52ms | 30.74/s |
| Clean Arch | Retorno Lista | 5000 | 738.33ms | 655.5ms | 1.18s | 1.63ms | 231.88ms | 106.25/s |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Hexagonal Arch | Registro | 5000 | 83.48ms | 71.31ms | 574.79ms | 3.99ms | 42.3ms | 1169.08/s |
| Hexagonal Arch | Login | 5000 | 50.81ms | 40.5ms | 271.75ms | 2.64ms | 24.25ms | 2031.99/s |
| Hexagonal Arch | Importação | 5000 | 2.52s | 2.26s | 3.93s | 1.99ms | 754.81ms | 31.07/s |
| Hexagonal Arch | Retorno Lista | 5000 | 586.44ms | 485.77ms | 3.27ms | 1.99ms | 188.45ms | 238.97/s |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| No Arch | Registro | 5000 | 79.52ms | 63ms | 739.41ms | 3.99ms | 41.11ms | 1205.45/s |
| No Arch | Login | 5000 | 40ms | 35ms | 109.43ms | 2ms | 21.04ms | 2331.94/s |
| No Arch | Importação | 5000 | 2.37s | 2.11s | 5.23s | 1.99ms | 728.51ms | 32.05/s |
| No Arch | Retorno Lista | 5000 | 224.58ms | 191.3ms | 688.24ms | 2ms | 96.76ms | 250.64/s |

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