import { useEffect, useState } from 'react';
import { FaSearch } from "react-icons/fa";

import './App.css';
import { capitalizeFirstLetter, getRightDate } from './services/utils';
import { Task } from './components/task/task';
import { ModalCreate } from './components/modalCreate/modalCreate';

function App() {
  //state para a busca de novos elementos
  const [search, setSearch] = useState('');
  //state para a visualização do modal
  const [modalCreate, setModalCreate] = useState(false);

  //state para a lista de tarefas
  const [list, setList] = useState([])
  //state para a lista de busca de tarefas
  const [searchList, setSearchList] = useState([])

  // personalizando o título para obter a data correta
  let week = capitalizeFirstLetter(getRightDate({ weekday: 'long' }));
  let day = capitalizeFirstLetter(getRightDate({ day: '2-digit' }))
  let month = capitalizeFirstLetter(getRightDate({ month: 'long' }))


  // função para buscar as tarefas apenas no caso de exitir algum valor dentro do input de busca
  function searchTasks() {
    if (search.length < 1) {
      return
    }

    let searchedList = list.filter(task => task.name.includes(search))

    setSearchList([
      ...searchedList
    ])
  }

  // quando a lista for alterada, adicionada ou deletada também buscar
  // realizar a função de busca toda vez que o input de busca for alterado
  useEffect(() => {
    searchTasks()
  }, [search, list])


  return (
    <main className="container">

      { // Apenas mostrar o modal de criação se o state para o modal for true
        modalCreate ?
          <ModalCreate list={list} setList={setList} setModal={setModalCreate} /> : null}

      <div className='main-list'>
        <h1>{week}, <span className='text-purple'>{day}</span> de {month}</h1>
        <label className='label-search' htmlFor='search'>
          <FaSearch />
          <input id='search' type='text' placeholder='Procurar tarefa' value={search} onChange={(ev) => setSearch(ev.target.value)} />
        </label>

        <div className='container-tasks'>
          {
            // se o valor de busca for maior que um mostrar a lista filtrada
            // senão mostrar toda a lista
            search.length > 0 ?
              searchList.map((item) => {
                return <Task key={item.id} item={item} list={list} setList={setList} />
              })
              :
              list.map((item) => {
                return <Task key={item.id} item={item} list={list} setList={setList} />
              })
          }
        </div>
      </div>
      <div className='container-button'>
        {/* ao clicar no botão aparecer o modal de nova tarefa */}
        <button className='button-new' onClick={() => setModalCreate(true)}>
          Nova tarefa
        </button>
      </div>
    </main>
  );
}

export default App;
