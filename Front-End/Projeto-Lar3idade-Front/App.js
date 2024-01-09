import React, { useState } from 'react';
import { StatusBar } from 'expo-status-bar';
import 'react-native-gesture-handler';
import { StyleSheet,Image,Text, View, TextInput, TouchableOpacity, ImageBackground,Button } from 'react-native';
import { createDrawerNavigator } from '@react-navigation/drawer';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationContainer} from '@react-navigation/native';
import UtenteScreen from './Telas/Utente.js';
import FamiliarScreen from './Telas/Familiar.js';
import Login from './Telas/Login.js'
import PrescricaoUtente  from './Telas/PrescricaoUtente.js';
import Visitas  from './Telas/Visitas.js';
import Consultas  from './Telas/Consultas.js';
import Atividades  from './Telas/Atividades.js';
import PlanoPagamento  from './Telas/PlanoPagamento.js';
import VisitasFamiliar from './Telas/VisitasFamiliar.js'
import PagamentosFamiliar from './Telas/PagamentosFamiliar.js'
import UtenteFamiliar from './Telas/UtenteFamiliar.js'


const Stack = createNativeStackNavigator();

const Drawer = createDrawerNavigator();


const UtenteDrawer = ({ navigation }) => {
  const handleSair = () => {
    navigation.navigate('Login');};
return(
  <Drawer.Navigator initialRouteName="Perfil" screenOptions={{headerStyle: {backgroundColor: '#71A1FF'}}}>
    <Drawer.Screen name="Perfil" component={UtenteScreen} options={{
    title: 'Perfil',
    headerRight: () => (
      <TouchableOpacity onPress= {handleSair} color="white" style={styles.sairButton}> 
      <Text style={styles.sairButtonText}>Sair</Text>
      </TouchableOpacity>
    ),
  }} />
    <Drawer.Screen name="Prescrições Médicas" component={PrescricaoUtente}
    
    
    />
    <Drawer.Screen name="Visitas" component={Visitas} />
    <Drawer.Screen name="Consultas" component={Consultas} />
    <Drawer.Screen name="Atividades" component={Atividades} />
    <Drawer.Screen name="Plano de pagamento" component={PlanoPagamento} />
  </Drawer.Navigator>
)};

const FamiliarDrawer = ({ navigation }) => {
  const handleSair = () => {
    navigation.navigate('Login');};
return(
  <Drawer.Navigator initialRouteName="FamiliarScreen" screenOptions={{headerStyle: {backgroundColor: '#71A1FF'}, headerTintColor: 'white',headerTitleStyle: {
   
    color: '#ffff', // Set your desired text color here
  },}}>
    <Drawer.Screen name="FamiliarScreen" component={FamiliarScreen} options={{
    title: 'Perfil',
    headerRight: () => (
      <TouchableOpacity onPress= {handleSair} color="white" style={styles.sairButton}> 
      <Text style={styles.sairButtonText}>Sair</Text>
      </TouchableOpacity>
    ),
  }}/>
    <Drawer.Screen name="VisitasFamiliar" component={VisitasFamiliar} options={{
    title: 'Visitas',
    headerRight: () => (
      <TouchableOpacity onPress= {handleSair} color="white" style={styles.sairButton}> 
      <Text style={styles.sairButtonText}>Sair</Text>
      </TouchableOpacity>
    ),
  }}/>
    <Drawer.Screen name="PagamentosFamiliar" component={PagamentosFamiliar}options={{
    title: 'Planos de Pagamento',
    headerRight: () => (
      <TouchableOpacity onPress= {handleSair} color="white" style={styles.sairButton}> 
      <Text style={styles.sairButtonText}>Sair</Text>
      </TouchableOpacity>
    ),
  }} />
    <Drawer.Screen name="UtenteFamiliar" component={UtenteFamiliar}options={{
    title: 'Familiares',
    headerRight: () => (
      <TouchableOpacity onPress= {handleSair} color="white" style={styles.sairButton}> 
      <Text style={styles.sairButtonText}>Sair</Text>
      </TouchableOpacity>
    ),
  }} />
  </Drawer.Navigator>
)};

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Login" screenOptions={{headerShown: false}} >
        <Stack.Screen name="Login" component={Login} />
        <Stack.Screen name="UtenteDrawer" component={UtenteDrawer} />
        <Stack.Screen name="FamiliarDrawer" component={FamiliarDrawer} />
      
      </Stack.Navigator>
    </NavigationContainer>
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
    paddingHorizontal: 150,
    borderRadius: 8,
    height: 40,
    marginBottom:35
  },
  loginButtonText: {
    color: '#fff',
    fontSize: 17,
    
    
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
    fontSize: 30,
    lineHeight: 60,
    textAlign: 'center',
    marginTop:110
  },
  sairButton: {
    backgroundColor: 'white',
    padding: 2,
    width:60,
    borderRadius: 5,
    marginRight: 10, 

  },
  sairButtonText: {
    color: 'black',
    fontWeight: 'bold',
    textAlign:'center'
  },
  
});

