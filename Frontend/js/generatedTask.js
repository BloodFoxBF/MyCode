const url = 'https://localhost:44388/api/Problem';
const sectionNotSolve = document.querySelector('.content-notsolve');

const getData = async function (url) {
    const response = await fetch(url);
    if (!response.ok) {
        throw new Error('Что-то пошло не так')
    }
    const data = response.json();
    return await data;
};

function addMoveTask (aboutTask) {
    const { ProblemId, Name, Legend} = aboutTask;
    const child = sectionNotSolve.querySelector('.done-content');
    const code = `
        <div class="time-block" data-products="${ProblemId}">
            <div class="block-title">
                <p id="block-title">Задача:</p>
                <span class=" block-descr">${Legend}</span>
            </div>
            <button class="button-tasks solve">Решить</button>
            <div class="block-mark">Оценка: 5</div>
        </div>
    `
    child.insertAdjacentHTML('beforeend', code);
}
getData(url).then( function (data) {
    data.forEach(addMoveTask);
})