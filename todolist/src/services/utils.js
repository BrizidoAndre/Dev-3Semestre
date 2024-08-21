// obter a data da maneira que o desenvolvedor mais julga necessário
export function getRightDate(object){
    let now = new Date();
    return now.toLocaleDateString('default',object)
}

// tornar a primeira letra da string maiúscula
export function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}