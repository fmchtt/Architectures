import http from 'k6/http';
import { check, fail } from 'k6';
import { SharedArray } from 'k6/data';
import { scenario } from 'k6/execution';
import { FormData } from 'https://jslib.k6.io/formdata/0.0.2/index.js'

const data = new SharedArray('users', () => {
  return JSON.parse(open('./user-faked.json'));
})

const xslx = open('./product-faked.xlsx', 'b');

export const options = {
  scenarios: {
    'uploadProdutos': {
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

  const fd = new FormData();
  fd.append('arquivo', http.file(xslx, 'product-faked.xlsx', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'))

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
      'Content-Type': 'multipart/form-data;  boundary=' + fd.boundary,
      'Authorization': `Bearer ${loginRes.json().token}`
    },
  };

  const productRes = http.post(urlProduto, fd.body(), paramsUploadProduto);

  check(productRes, {
    'product creation is working': (r) => r.status === 200
  });
}