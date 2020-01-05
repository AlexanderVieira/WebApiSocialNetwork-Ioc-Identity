node("master") {

	try {	         


		stage('SCM') {

			git branch: 'master', 
			credentialsId: 'alexandervieira', 
			url: 'https://github.com/AlexanderVieira/WebApiSocialNetwork-Ioc-Identity.git'
		}

		stage('Mvn Package'){

			sh 'mvn clean package'
		}

		stage('SonarQube analysis') {

			withSonarQubeEnv('sonarqube-server') {

				sh 'mvn sonar:sonar -Dsonar.projectKey=managerproject-back -Dsonar.host.url=http://127.0.0.1:9000 -Dsonar.login=b1eb6a42b659db14d3adfdfff43df80f9f7c8990'

			}

		}

		stage('Email Sucess')
		{

			//emailext (
			//	to: 'alexander.silva.developer@gmail.com',
			//	subject: "Sucess Pipeline: ${currentBuild.fullDisplayName}",
			//	body: "Sucess with ${env.BUILD_URL}"
			//	)	 
		}



	}catch (e) {

		stage('Email Failed')
		{

			emailext (
				to: 'alexander.silva.developer@gmail.com, asilva943rj@gmail.com',
				subject: "Failed Pipeline: ${currentBuild.fullDisplayName}",
				body: "Something is wrong with ${env.BUILD_URL}"
				)	 
		}


		throw e
	}
	
}