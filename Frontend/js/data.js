'use strict'

const select = document.querySelector('.check-language')

const lang = JSON.stringify(select.value);
//const url = 'https://jsonplaceholder.typicode.com/users';
const url = 'https://localhost:44388/api/Students';

const getData = async function (url) {
    const response = await fetch(url);
    if (!response.ok) {
        throw new Error('Что-то пошло не так')
    }
    const data = response.json();
    return await data;
};

getData(url).then(function (data) {
    console.log(data);
})

/*fetch(url)
  .then(function (response)  {
      return response.JSON
  })
  .then(function (data) {
      console.log(data)
  })*/