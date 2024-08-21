import { useState } from "react";


export const ModalAlter = ({item, list ,setList, setModal}) => {

    // state para o nome que será alterado da tarefa
    // por padrão ele recebe o nome antigo
    const [newTask, setNewTask] = useState(item.name)

    // altera apenas o nome do elemento selecionado
    // mais nenhuma propriedade é alterada
    function alterTask(){
        let index = list.findIndex((element)=>element.id === item.id)
        list[index] = {
            id: item.id,
            name: newTask,
            status: item.status
        }
        setList([
            ...list
        ])
        setModal(false)
    }

    return(
        <form onSubmit={alterTask} className='main-list modal-background'>
            <h2>Alter task</h2>

            <textarea className='input-create' value={newTask} onChange={(ev)=>setNewTask(ev.target.value)}/> 
            <button className='button-create' type='submit'>Alterar tarefa</button>
        </form>
    )
}