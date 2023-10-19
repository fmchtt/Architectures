import http from 'k6/http';
import { check, fail } from 'k6';
import { SharedArray } from 'k6/data';
import { scenario } from 'k6/execution';

const data = new SharedArray('users', () => {
  return JSON.parse(open('./data/user-faked.json'));
})

export const options = {
  scenarios: {
    'listProducts': {
      executor: 'shared-iterations',
      iterations: data.length,
      vus: 50,
    },
  },
};

export default function () {
  const urlLogin = 'http://localhost/usuario/entrar/';
  const urlProduto = 'http://localhost/produtos/';

  const user = data[scenario.iterationInTest];

  const paramsLogin = {
    headers: {
      'Content-Type': 'application/json'
    },
  };

  const loginRes = http.post(urlLogin, JSON.stringify(user), paramsLogin);

  check(loginRes, {
    'user login successfull': (r) => r.status === 200,
    'user token returned': (r) => r.status === 200 && r.json().token
  }) || fail(`Erro ao efetuar login do usuario: ${user.nome} com a senha: ${user.senha}, resposta: ${loginRes.body}`)

  const paramsUploadProduto = {
    headers: {
      'Authorization': `Bearer ${loginRes.json().token}`
    },
  };

  const productRes = http.get(urlProduto, paramsUploadProduto);

  check(productRes, {
    'product list is working': (r) => r.status === 200,
    'product amount is correct': (r) => r.status === 200 && r.json().length === 1000,
  }) || fail(`Erro ao efetuar listagem dos productos: ${user.nome} com a senha: ${user.senha}, resposta: ${productRes.body}`);
}