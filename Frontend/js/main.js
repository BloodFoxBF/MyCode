const logout = document.querySelector('.button-logout');
const cardButton = document.querySelector('.card-button');
const sectionMarks = document.querySelector('.sections-marks');
const sectionTasks = document.querySelector('.sections-tasks');
const arrowDown = document.querySelector('.arrowDown');
const solve = document.querySelector('.solve');

// FIRSTSTUDENT

if (document.location.pathname == '/Frontend/firstStudent.html') {

    function logoutFrom() {
        document.location.href = 'index.html';
    };
    
    function goToMarks (event) {
        const target = event.target;
        const Marks = target.closest('.sections-marks');
        if (Marks) {
            document.location.href = 'myMarks.html';
        }
    };

    function goToTasks (event) {
        const target = event.target;
        const Marks = target.closest('.sections-tasks');
        if (Marks) {
            document.location.href = 'tasks.html';
        }
    }

    sectionMarks.addEventListener('click', goToMarks);
    logout.addEventListener('click', logoutFrom);
    sectionTasks.addEventListener('click', goToTasks);
};


// MYMARKS

if (document.location.pathname == '/Frontend/myMarks.html') {

    const arrow = document.querySelector('.arrow');
    
    function goToTask () {
        document.location.href = 'aboutTask.html';
    };

    function goToBack () {
        document.location.href = 'firstStudent.html';
    };
    
    arrow.addEventListener('click', goToBack);
    cardButton.addEventListener('click', goToTask);
};


// TASK


if (document.location.pathname == '/Frontend/tasks.html') {
    const arrow = document.querySelector('.arrow');

    function goToSolve (event) {
        const target = event.target
        const child = target.closest('.solve')
        if (child){
            const parent = child.parentElement;
            let productsId = parent.dataset.products;
            localStorage.setItem('numberTasks', productsId);
            document.location.href = 'solveTask.html';
        }
    }

    function goToBack () {
        document.location.href = 'firstStudent.html';
    };
    
    //arrowDown.addEventListener('click', downBlock);
    window.addEventListener('click', goToSolve);
    arrow.addEventListener('click', goToBack);
};


// SOLVE TASK


if (document.location.pathname == '/Frontend/solveTask.html') {
    const url = `https://localhost:44388/api/Problem/${localStorage.getItem('numberTasks')}`;
    const urlPost = `https://localhost:44388/api/Problem`;
    const arrow = document.querySelector('.arrow');
    const buttonStudentTask = document.querySelector('.send');
    
    const getData = async function (url) {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error('Что-то пошло не так')
        }
        const data = response.json();
        return await data;
    };

    function generatedTask (taskInfo) {
        const {Name, Legend} = taskInfo;
        const titleTask = document.querySelector('.solve-task');
        const taskDescription = document.querySelector('.task-descr');

        titleTask.textContent = Name;
        taskDescription.textContent = Legend;
    };

    getData(url).then(function (data) {
        generatedTask(data)
    });

    function onClickGetData () {
        const areaUser = document.querySelector('.code-task').value;
        const codeOutput = document.querySelector('.code-output');
        if (areaUser != '') {
            const postData = fetch (urlPost, {
                method: 'POST',
                headers: {'Content-Type': 'text/plain', 
                'id': '1', 
                'userfunction': areaUser}
                }).then(function (response) {
                    return response
                }).then(function (data) {
                    codeOutput.textContent = data;
                })
        }
    };

    function goToBack () {
        document.location.href = 'tasks.html';
    };

    arrow.addEventListener('click', goToBack);
    buttonStudentTask.addEventListener('click', onClickGetData);
};