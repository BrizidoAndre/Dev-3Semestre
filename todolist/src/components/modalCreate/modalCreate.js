import { useState } from 'react'
import './modalCreate.css'

export const ModalCreate = ({list, setList, setModal}) => {

    // state para o texto da nova tarefa
    const [valueCreate, setValueCreate] = useState('')

    // cria uma nova tarefa tendo como padrão o status negativo e um id aleatório
    function createTask(ev){
        ev.preventDefault();
        let newList = list;
        newList.push({
            id:Math.random(),
            name:valueCreate,
            status:false
        })

        setList([
            ...newList
        ])

        setModal(false);
    }

    return (
        <form onSubmit={createTask} className='main-list modal-background'>
            <h2>Descreva sua tarefa</h2>

            <textarea className='input-create' value={valueCreate} onChange={(ev)=>setValueCreate(ev.target.value)}/> 
            <button className='button-create' type='submit'>Confirmar tarefa</button>
        </form>
    )
}