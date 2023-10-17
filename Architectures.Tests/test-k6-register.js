import http from 'k6/http';
import { check } from 'k6';
import { sleep } from 'k6';
import { SharedArray } from 'k6/data';
import { scenario } from 'k6/execution';

const data = new SharedArray('register', () => {
    return JSON.parse(open('./user-faked.json'));
})

export const options = {
    scenarios: {
      'use-all-the-data': {
        executor: 'shared-iterations',
        vus: 50,
        iterations: data.length,
        maxDuration: '1h'
      },
    },
  };

export default function () {
    const url = 'http://localhost/usuario/registrar';
    const params = {
        headers: {
            'Content-Type': 'application/json'
            // 'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InB1Ymxpc2hlcm54QG56bi5pbyIsImp0aSI6IjE1OTMiLCJzdWIiOiJQdWJsaXNoZXIgTlgiLCJuYmYiOjE2ODg3NjM3NTAsImV4cCI6MTY4ODkzNjU1MCwiaWF0IjoxNjg4NzYzNzUwLCJpc3MiOiJOWk4iLCJhdWQiOiJuZXhwZXJ0cy1hcHAifQ.GKNAvBrqCBuDRCgB7LhKXlDeoCzCtsMWCayxGS-MMkY'
        },
    };

    const user = data[scenario.iterationInTest];
    
    const res = http.post(url, JSON.stringify(user), params);

    console.log(res);

    const checkRes = check(res, {
        'status is 200': (r) => r.status === 200
    });

    
}