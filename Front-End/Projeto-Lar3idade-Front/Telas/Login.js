// Importe os módulos necessários
import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import 'react-native-gesture-handler';
import { StyleSheet, Image, Text, View, TextInput, TouchableOpacity, ImageBackground } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import axios from 'axios';

export default function Login({ route, navigation }) {
  const [tableData, setTableData] = useState([]);
  const [textEmail, setTextEmail] = useState("");
  const [textPass, setTextPass] = useState("");
  const [showPassword, setShowPassword] = useState(false); 

  const handleLogin = () => {
    axios.get('http://192.168.1.80:8800/utente')
      .then(utenteResponse => {
        if (utenteResponse.data.length > 0) {

          const utenteEmailColumn = utenteResponse.data.map(entry => entry.email);
          const utenteSenhaColumn = utenteResponse.data.map(entry => entry.senha);
          const utenteNomeColumn = utenteResponse.data.map(entry => entry.nome);

          const inputValueEmail = textEmail.trim();
          const inputValueSenha = textPass.trim();

          const isUtenteMatch = utenteEmailColumn.some((email, index) => email === inputValueEmail && utenteSenhaColumn[index] === inputValueSenha);
          const utenteMatch = utenteResponse.data.find(entry => entry.email === inputValueEmail && entry.senha === inputValueSenha);
          console.log(isUtenteMatch);
          if (isUtenteMatch) {
            console.log(isUtenteMatch);
            console.log('Login com utente!');
            console.log('Email e Senha:', inputValueEmail, inputValueSenha);
            console.log('UtenteData:', utenteMatch);
            console.log('Nome do Utente:', utenteMatch.nome);
            navigation.navigate('UtenteDrawer', { screen: 'Home Utente', params: { utenteData: utenteMatch, utenteNome: utenteMatch.nome} });
            
          } else {
            axios.get('http://192.168.1.80:8800/familiar')
              .then(familiarResponse => {
                if (familiarResponse.data.length > 0) {
                  const familiarEmailColumn = familiarResponse.data.map(entry => entry.email);
                  const familiarSenhaColumn = familiarResponse.data.map(entry => entry.senha);

                  const isFamiliarMatch = familiarEmailColumn.some((email, index) => email === inputValueEmail && familiarSenhaColumn[index] === inputValueSenha);
                  const FamiliarMatch = familiarResponse.data.find(entry => entry.email === inputValueEmail && entry.senha === inputValueSenha);

                  if (isFamiliarMatch) {
                    console.log('Login como Familiar');
                    console.log('Email e Senha:', inputValueEmail, inputValueSenha);
                    console.log('UtenteData:', FamiliarMatch);
                    console.log('Nome do Utente:', FamiliarMatch.nomel);
                    navigation.navigate('FamiliarDrawer', { screen: 'FamiliarScreen', params: { FamiliarData: FamiliarMatch, FamiliarNome: FamiliarMatch.nomel } });
                    // Se o login for bem-sucedido, navegue para o FamiliarDrawer
                    navigation.navigate('FamiliarDrawer');
                  } else {
                    console.log('Login inválido para ambos');
                  }
                } else {
                  console.log('Tabela Familiar vazia');
                }
              })
              .catch(error => {
                console.error('Erro ao buscar dados do familiar:', error);
              });
          }
        } else {
          console.log('Tabela Utente vazia');
        }
      })
      .catch(error => {
        console.error('Erro ao buscar dados do utente:', error);
      })
      .finally(() => {
        clearTextInputs();
      });
  };

  const clearTextInputs = () => {
    setTextEmail("");
    setTextPass("");
  };

  const handleInputChangeEmail = (inputMail) => {
    setTextEmail(inputMail);
  };

  const handleInputChangeSenha = (inputSenha) => {
    setTextPass(inputSenha);
  };

  return (
    <View style={styles.container}>
      <ImageBackground source={require('../Image/Image3.png')} resizeMode="cover" style={styles.image3}>
        <Text style={styles.text}>Plataforma para Gestão de Fichas de Utentes em Lares</Text>
      </ImageBackground>
      <View>
        <Image source={require('../Image/Logo.jpg')} style={styles.logo} />
      </View>
      <View style={styles.inputContainer}>
        <Text style={styles.login}>Login</Text>
        <TextInput
          style={styles.input}
          placeholder="E-mail"
          keyboardType="email-address"
          autoCapitalize="none"
          onChangeText={handleInputChangeEmail}
          value={textEmail}
        />
        <TextInput
          style={styles.input}
          placeholder="Senha"
          secureTextEntry={!showPassword}
          onChangeText={handleInputChangeSenha}
          value={textPass}
        />
        <TouchableOpacity
      style={styles.showPasswordButton}
      onPress={() => setShowPassword(!showPassword)}>
      <Text style={styles.showPasswordText}>
        {showPassword ? 'Ocultar' : 'Mostrar'}
      </Text>
    </TouchableOpacity>
      </View>
      
      <TouchableOpacity style={styles.loginButton} onPress={handleLogin}>
        <Text style={styles.loginButtonText}>Entrar</Text>
      </TouchableOpacity>

      <StatusBar style="auto" />
      <View>
        <Image source={require('../Image/Image2.png')} style={styles.Image2} />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
    marginBottom:-440
  },
  passwordContainer: {
    position: 'relative',
  },
  showPasswordButton: {
    position: 'absolute',
    top: 120, 
    right: 10, 
  },
  login: {
    fontSize: 24,
    marginBottom: 24,
    textAlign:'center'
  },
  inputContainer: {
    width: '100%',
    marginBottom: 16,
  },
  input: {
    height: 40,
    borderColor: 'gray',
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 16,
    marginBottom: 16,
    
  },
  loginButton: {
    backgroundColor: '#3498db',
    paddingVertical: 9,
    paddingHorizontal: 140,
    borderRadius: 8,
    height: 40,
    marginBottom:35
  },
  loginButtonText: {
    color: '#fff',
    fontSize: 17,
    width: '100%', 
  },
  logo:{
    width: 100,  
    height: 100, 
    resizeMode: 'contain',
    marginBottom: 20,
    alignItems: 'center'
  },
  Image2:{
    padding:107,
    width:440,
  },
  image3:{
    width:400,
    height:350,
    marginTop:-400
  },
  text: {
    color: 'black',
    fontSize: 22,
    lineHeight: 60,
    textAlign: 'center',
    marginTop:110,
    padding:10
  },

  
});

