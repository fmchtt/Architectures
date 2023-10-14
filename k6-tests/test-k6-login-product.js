import http from 'k6/http';
import { check } from 'k6';
import { sleep } from 'k6';
import { SharedArray } from 'k6/data';
import { scenario } from 'k6/execution';
import { FormData } from 'https://jslib.k6.io/formdata/0.0.2/index.js'

const data = new SharedArray('register', () => {
    return JSON.parse(open('./user-faked-login.json'));
})

const xslx = open('./product-faked.xlsx', 'b');

export const options = {
    scenarios: {
      'login': {
        executor: 'shared-iterations',
        vus: 10,
        iterations: data.length,
        maxDuration: '1h'
      },
    },
  };

export default function () {
    const url = 'http://localhost/usuario/entrar';
    const urlProduto = 'http://localhost/produtos';

    const userLogin = data[scenario.iterationInTest];
    
    const fd = new FormData();
    fd.append('data', http.file(xslx, 'product-faked.xlsx', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'))
    
    const paramsLogin = {
      headers: {
          'Content-Type': 'application/json'
      },
    };

    const resLogin = http.post(url, JSON.stringify(userLogin), paramsLogin);

    const checkResLogin = check(resLogin, {
      'status is 200': (r) => r.status === 200
    });

    if(resLogin.status === 200){
      const resLoginJson = JSON.parse(resLogin.body);
      console.log(resLoginJson.token)
      const params = {
          headers: {
              'Content-Type': 'multipart/form-data;  boundary=' + fd.boundary,
              'Authorization': `Bearer ${resLoginJson.token}`
          },
      };

      const res = http.post(urlProduto, fd.body(), params);

      const checkResProd = check(res, {
        'status is 200': (r) => r.status === 200
      });
    }
    
}