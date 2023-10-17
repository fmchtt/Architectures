import http from 'k6/http';
import { check, fail } from 'k6';
import { SharedArray } from 'k6/data';
import { scenario } from 'k6/execution';

const data = new SharedArray('users', () => {
  return JSON.parse(open('./user-faked.json'));
})

export const options = {
  scenarios: {
    'login': {
      executor: 'shared-iterations',
      iterations: data.length,
      vus: 50,
    },
  },
};

export default function () {
  const url = 'http://localhost/usuario/entrar/';

  const params = {
    headers: {
      'Content-Type': 'application/json'
    },
  };

  const user = data[scenario.iterationInTest];

  const loginRes = http.post(url, JSON.stringify(user), params);

  check(loginRes, {
    'user login successfull': (r) => r.status === 200,
    'user login returned token': (r) => r.status === 200 && r.json().token
  }) || fail(`Erro ao efetuar login do usuario: ${user.nome} com a senha: ${user.senha}, resposta: ${loginRes.body}`);
}