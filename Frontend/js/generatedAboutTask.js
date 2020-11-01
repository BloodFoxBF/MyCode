const contentTask = document.querySelector('.content-task');
const url = `https://localhost:44388/api/Problem`;

const getData = async function (url) {
    const response = await fetch(url);
    if (!response.ok) {
        throw new Error('Что-то пошло не так')
    }
    const data = response.json();
    return await data;
};

getData(url).then(function (data) {
    console.log('Данные получены');
})

function generatedTask () {
    const code = `
    <div class="solve-title">
                        <h2 class="task-title">Задача:</h2>
                        <p class="task-descr">Используя html разметку, выведите на сайт блок, который будет содержать в себе текст Hello world!
                        </p>
                    </div>
                    <div class="task-another-file">
                        <p class="file-title task-title">Доп. файлы для решения:</p>
                        <div class="file-buttons">
                            <button class="task-file">style.css</button>
                            <button class="task-file">style.css</button>
                            <button class="task-file">style.css</button>
                        </div>
                    </div>
                    <div class="task-input">
                        <p class="input-title task-title">Ввод:<span class="task-descr" >20 30.</span></p> 
                        <p class="output-title task-title">Вывод:<span class="task-descr">2030.</span></p> 
                    </div>
                    <div class="task-language">
                        <p class="language-title task-title">Язык программирования:</p>
                        <select class="check-language" name="check-language">
                            <option value="Csharp">C#</option>
                            <option value="Html">Html</option>
                            <option value="Css">Css</option>
                        </select>
                    </div>
                    <div class="task-code">
                        <div class="code-line">
                            <p class="task-title">Решение задачи:</p>
                            <button class="code-file">Прикрепить файл <img src="img/" alt=""> </button>
                        </div>
                        <textarea class="code-task" name="code-task" id="" placeholder="Введите свой код сюда"></textarea>
                    </div>
                    <div class="task-buttons">
                        <button class="task-button check">Проверить</button>
                        <button class="task-button send">Отправить</button>
                    </div>
    `;
    contentTask.insertAdjacentHTML('beforeend', code);
}
generatedTask();

const buttonStudentTask = document.querySelector('.send');




function onClickGetData () {
    const getData = async function (url) {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error('Что-то пошло не так')
        }
        const data = response.json();
        return await data;
    };
    
    getData(url).then(function (data) {
        console.log('Данные отправлены');
    })
}
buttonStudentTask.addEventListener('click', onClickGetData);